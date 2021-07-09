using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using KIP_Backend.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace KIP_server_GET
{
    /// <summary>
    /// KIP_server_GET startup.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        private readonly bool enableSwagger;
        private readonly bool enableTokens;

        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">Configuration.</param>
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;

            this.enableSwagger =
                (this.Configuration["EnableSwagger"]?.Equals("true", StringComparison.InvariantCultureIgnoreCase)).GetValueOrDefault();

            this.enableTokens =
                (this.Configuration["Tokens:EnableTokens"]?.Equals("true", StringComparison.InvariantCultureIgnoreCase)).GetValueOrDefault();
        }

        private IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">The services.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            Console.OutputEncoding = System.Text.Encoding.Default;

            services.AddMvcCore()
                .AddDataAnnotations()
                .AddApiExplorer()
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.SuppressMapClientErrors = true;
                    options.InvalidModelStateResponseFactory = context => new BadRequestObjectResult(context.ModelState);
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            var pgConnectionString = this.Configuration.GetConnectionString("PostgresConnection");
            var pgVersionString = this.Configuration.GetConnectionString("PostgresVersion");
            services.AddDbServices(pgConnectionString, pgVersionString);

            services.AddApiVersioning(o =>
            {
                o.ReportApiVersions = true;
            })
                .AddVersionedApiExplorer(options =>
                {
                    options.GroupNameFormat = "'v'VVV";
                    options.SubstituteApiVersionInUrl = true;
                });

            if (this.enableSwagger)
            {
                services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>()
                    .AddSwaggerGen();
            }
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">Application.</param>
        /// <param name="logger">Logger.</param>
        /// <param name="env">Environment.</param>
        /// <param name="apiDescriptionProvider">Api Description Provider.</param>
        public void Configure(
            IApplicationBuilder app,
            ILogger<Startup> logger,
            IWebHostEnvironment env,
            IApiVersionDescriptionProvider apiDescriptionProvider)
        {
            app.UseHttpsRedirection();
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

            if (this.enableTokens)
            {
                app.UseTokens(this.Configuration["Tokens:EntryToken"]);
                var message = $"{Assembly.GetEntryAssembly().GetName().Name} uses Tokens Protection";
                logger.Log(LogLevel.Information, message);
            }

            if (this.enableSwagger)
            {
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    // build a swagger endpoint for each discovered API version
                    foreach (var description in apiDescriptionProvider.ApiVersionDescriptions)
                    {
                        options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                    }

                    app.Map("/swagger/versions_info", builder => builder.Run(async context =>
                        await context.Response.WriteAsync(
                            string.Join(Environment.NewLine, options.ConfigObject.Urls.Select(
                                descriptor => $"{descriptor.Name} {descriptor.Url}")))));
                });
            }

            app.UseEndpoints(builder =>
            {
                builder.MapControllers();
            });

            app.UseResponseCaching();
        }
    }
}
