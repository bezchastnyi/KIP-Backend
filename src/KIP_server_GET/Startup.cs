using System;
using System.Diagnostics.CodeAnalysis;
using KIP_server_GET.Constants;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace KIP_server_GET
{
    /// <summary>
    /// 
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        private IConfiguration Configuration { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">Configuration.</param>
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services">The services.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            Console.OutputEncoding = System.Text.Encoding.Default;

            services.AddMvc();

            var pgConnectionString = this.Configuration.GetConnectionString("PostgresConnection");
            var pgVersionString = this.Configuration.GetConnectionString("PostgresVersion");
            services.AddDbServices(pgConnectionString, pgVersionString);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="app"></param>
        /// <param name="logger"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, ILogger<Startup> logger, IWebHostEnvironment env)
        {
            app.UseTokens(this.Configuration["Tokens:EntryToken"]);
            var message = $"{CustomNames.KIP_server_GET} uses Tokens Protection";
            logger.Log(LogLevel.Information, message);

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

            app.UseEndpoints(builder =>
            {
                builder.MapControllers();
            });

            app.UseResponseCaching();
        }
    }
}
