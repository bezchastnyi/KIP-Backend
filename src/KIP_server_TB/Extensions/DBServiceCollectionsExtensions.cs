using System;
using KIP_server_TB.DB;
using Microsoft.EntityFrameworkCore;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Contains DB related extension methods.
    /// </summary>
    public static class DbServiceCollectionsExtensions
    {
        private const string NullOrEptyErrorMessage = "{0} must not be null or empty";

        /// <summary>
        /// Adds the database services.
        /// </summary>
        /// <returns>Services.</returns>
        /// <param name="services">The services.</param>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="pgVersionString">Postgres version string. Must contain 2-4 numerics separated by '.'.</param>
        /// <exception cref="ArgumentNullException">Services.</exception>
        /// <exception cref="ArgumentException">Connection string must be not null or empty - connectionString.</exception>
        public static IServiceCollection AddDbServices(this IServiceCollection services, string connectionString, string pgVersionString)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentException(string.Format(NullOrEptyErrorMessage, nameof(connectionString)));
            }

            if (string.IsNullOrEmpty(pgVersionString))
            {
                throw new ArgumentException(string.Format(NullOrEptyErrorMessage, nameof(pgVersionString)));
            }

            var pgVersion = new Version(pgVersionString);
            services.AddDbContextPool<TelegramDbContext>(contextOptions =>
            {
                contextOptions.UseNpgsql(connectionString, npgOptions =>
                {
                    npgOptions.MigrationsAssembly("KIP_telegram_bot")
                        .EnableRetryOnFailure();
                    npgOptions.SetPostgresVersion(pgVersion);
                });
            });

            return services;
        }
    }
}
