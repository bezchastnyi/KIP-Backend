using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace KIP_server_NoAuth
{
    /// <summary>
    /// The server GET.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class Program
    {
        /// <summary>
        /// Entry point.
        /// </summary>
        /// <param name="args">The arguments.</param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Creates the web host builder.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns>Configured web host builder.</returns>
        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
            var url = $"https://0.0.0.0:{port}";

            return Host.CreateDefaultBuilder(args)
                    .UseSerilog(
                        (context, configuration) =>
                    {
                        configuration.ReadFrom.Configuration(context.Configuration);
                    }, true)
                    .ConfigureWebHostDefaults(webBuilder =>
                    {
                        webBuilder.ConfigureKestrel(serverOptions =>
                        {
                            serverOptions.AddServerHeader = false;
                        })
                        .UseStartup<Startup>().UseUrls(url);
                    });
        }
    }
}


/*
    GCP DEPLOY

    cd \Users\IT\source\Repos\KIP-Backend-DB\KIP_server_GET
    gcloud builds submit --tag gcr.io/khpi-in-phone-307713/kipserverget
    gcloud run deploy --image gcr.io/khpi-in-phone-307713/kipserverget --platform managed
 */
