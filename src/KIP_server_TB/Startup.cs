using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using KIP_Backend.Constants;
using KIP_server_TB.DB;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Telegram.Bot;

namespace KIP_server_TB
{
    /// <summary>
    /// KIP_server_TB startup.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        private static readonly string AssemblyName = Assembly.GetEntryAssembly()?.GetName().Name;
        private TelegramBotClient _telegramBotClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">Configuration.</param>
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        private IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">The services.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            Console.OutputEncoding = System.Text.Encoding.Default;

            var pgConnectionString = this.Configuration.GetConnectionString("PostgresConnection");
            var pgVersionString = this.Configuration.GetConnectionString("PostgresVersion");
            services.AddDbServices<TelegramApiDbContext>(pgConnectionString, pgVersionString);

            services.AddMvcCore()
                .AddDataAnnotations()
                .AddApiExplorer()
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.SuppressMapClientErrors = true;
                    options.InvalidModelStateResponseFactory = context => new BadRequestObjectResult(context.ModelState);
                })
                .AddNewtonsoftJson()
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddApiVersioning(o =>
            {
                o.ReportApiVersions = true;
            })
                .AddVersionedApiExplorer(options =>
                {
                    options.GroupNameFormat = "'v'VVV";
                    options.SubstituteApiVersionInUrl = true;
                });

            var telegramConnectionString = this.Configuration.GetConnectionString("TelegramConnection");
            if (string.IsNullOrEmpty(telegramConnectionString))
            {
                throw new ArgumentException(string.Format(BackendConstants.NullOrEmptyErrorMessage, nameof(telegramConnectionString)));
            }

            this._telegramBotClient = new TelegramBotClient(telegramConnectionString);
            services.AddSingleton<ITelegramBotClient>(this._telegramBotClient);
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="env">The environment.</param>
        public void Configure(IApplicationBuilder app, ILogger<Startup> logger, IWebHostEnvironment env)
        {
            // app.UseHttpsRedirection();
            app.UseRouting();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePages();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            if (this._telegramBotClient != null)
            {
                logger.LogInformation($"{AssemblyName} starts listening {this._telegramBotClient.GetMeAsync().Result.Username}");
            }

            app.UseEndpoints(builder =>
            {
                builder.MapControllers();
            });

            app.UseResponseCaching();
        }
    }
}
