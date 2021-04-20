// <copyright file="KIP_POST_APPServiceCollectionExtensions.cs" company="KIP">
// Copyright (c) KIP. All rights reserved.
// </copyright>

using System;
using KIP_POST_APP.DB;
using KIP_POST_APP.Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// The collection extensions for TransferDataBQToS3 services.
    /// </summary>
    public static class KIP_POST_APPServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the TransferDataBQToS3 services.
        /// </summary>
        /// <returns>
        /// Services.
        /// </returns>
        /// <param name="services">The service collection.</param>
        /// <param name="config">The configuration.</param>
        public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration config)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (config == null)
            {
                throw new ArgumentNullException(nameof(config));
            }

            Console.OutputEncoding = System.Text.Encoding.Default;
            services.AddAutoMapper(typeof(MapperProfile));

            var connectionString = config["ConnectionStrings:PostgresConnection"];
            var pgVersion = new Version(config["ConnectionStrings:PostgresVersion"]);

            services.AddDbContext<ServerContext>(
                contextOptions =>
            {
                contextOptions.UseNpgsql(connectionString, npgOptions =>
                {
                    npgOptions.MigrationsAssembly("KIP_POST_APP")
                        .EnableRetryOnFailure();
                    npgOptions.SetPostgresVersion(pgVersion);
                });
            }, ServiceLifetime.Singleton);

            return services;
        }
    }
}
