using System;
using KIP_auth_mode.Constants;
using KIP_auth_mode.Mapping;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace KIP_auth_mode
{
    /// <summary>
    /// KIP_auth_mode startup.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">Configuration.</param>
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">The services.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            Console.OutputEncoding = System.Text.Encoding.Default;
            services.AddMvc();
            services.AddAutoMapper(typeof(MapperProfile));
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">Application.</param>
        /// <param name="logger">Logger.</param>
        /// <param name="env">Environment.</param>
        public void Configure(IApplicationBuilder app, ILogger<Startup> logger, IWebHostEnvironment env)
        {
            app.UseTokens(this.Configuration["Tokens:EntryToken"]);
            var message = $"{CustomNames.KIP_auth_mode} uses Tokens Protection";
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
