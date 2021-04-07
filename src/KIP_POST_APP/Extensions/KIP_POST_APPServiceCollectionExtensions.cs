using Microsoft.Extensions.Configuration;
using System;
using KIP_POST_APP.Mapping;
using Microsoft.Extensions.Configuration;
using KIP_server_GET.DB;
using Microsoft.EntityFrameworkCore;
using KIP_POST_APP.Services;

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
            services.AddDbContext<ServerContext>(
                opts => opts.UseNpgsql(connectionString), ServiceLifetime.Singleton
            );


            return services;
        }
    }
}
