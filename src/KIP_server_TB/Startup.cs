using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using KIP_POST_APP.DB;
using KIP_server_TB.DB;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace KIP_server_TB
{
    /// <summary>
    /// KIP_telegram_bot startup.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        private readonly string assenmblyName = Assembly.GetEntryAssembly()?.GetName().Name;

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
            services.AddDbServices<TelegramDbContext>(pgConnectionString, pgVersionString, this.assenmblyName);
            services.AddDbServices<PostDbContext>(pgConnectionString, pgVersionString, this.assenmblyName);

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
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">Application.</param>
        /// <param name="logger">Logger.</param>
        /// <param name="env">Environment.</param>
        public void Configure(
            IApplicationBuilder app,
            ILogger<Startup> logger,
            IWebHostEnvironment env)
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

            app.UseEndpoints(builder =>
            {
                builder.MapControllers();
            });

            app.UseResponseCaching();
        }
    }
}
