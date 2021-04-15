// <copyright file="Program.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace KIP_POST_APP
{
    /// <summary>
    /// The console app class.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The main method.
        /// </summary>
        /// <param name="args">The console app args.</param>
        /// <returns>Th task.</returns>
        public static async Task Main(string[] args)
        {
            await CreateHostBuilder(args).RunConsoleAsync();
        }

        /// <summary>
        /// Creates host builder.
        /// </summary>
        /// <param name="args">The command line arguments.</param>
        /// <returns>Configured host builder.</returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .UseSerilog(
                (context, configuration) =>
                {
                    configuration.ReadFrom.Configuration(context.Configuration);
                },
                true)
            .ConfigureServices(
                (context, services) =>
                {
                    services.ConfigureServices(context.Configuration);
                    services.AddHostedService<KIP_POST_APPHostedService>();
                });
    }
}
