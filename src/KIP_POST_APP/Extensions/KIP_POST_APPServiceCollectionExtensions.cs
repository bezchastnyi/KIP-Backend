using KIP_POST_APP.DB;
using KIP_POST_APP.Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// The collection extensions for TransferDataBQToS3 services.
    /// </summary>
    public static class KIP_POST_APPServiceCollectionExtensions
    {
        private const string InvalidOperationExceptionMsg = "{0} is null or empty; Application cannot be started.";
        private const string ErrorForUnsupportedMessagePattern = "'{0}' {1} is not supported; Application cannot be started.";

        /// <summary>
        /// Adds the TransferDataBQToS3 services.
        /// </summary>
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
            var pgVersionString = config["ConnectionStrings:PostgresVersion"];
            var pgVersion = new Version(pgVersionString);
            services.AddDbContext<ServerContext>(contextOptions =>
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
