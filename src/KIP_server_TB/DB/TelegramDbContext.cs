using KIP_server_TB.Models;
using Microsoft.EntityFrameworkCore;

namespace KIP_server_TB.DB
{
    /// <summary>
    /// Server context.
    /// </summary>
    public class TelegramDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TelegramDbContext"/> class.
        /// </summary>
        /// <param name="options">DB context options.</param>
        public TelegramDbContext(DbContextOptions<TelegramDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Gets or sets the audience.
        /// </summary>
        /// <value>Audience.</value>
        public virtual DbSet<TelegramUser> Users { get; set; }

        /// <summary>
        /// Override this method to further configure the model that was discovered by convention from the entity types
        /// exposed in <see cref="T:Microsoft.EntityFrameworkCore.DbSet`1" /> properties on your derived context. The resulting model may be cached
        /// and re-used for subsequent instances of your derived context.
        /// </summary>
        /// <param name="modelBuilder">The builder being used to construct the model for this context. Databases (and other extensions) typically
        /// define extension methods on this object that allow you to configure aspects of the model that are specific
        /// to a given database.</param>
        /// <remarks>
        /// If a model is explicitly set on the options for this context (via <see cref="M:Microsoft.EntityFrameworkCore.DbContextOptionsBuilder.UseModel(Microsoft.EntityFrameworkCore.Metadata.IModel)" />)
        /// then this method will not be run.
        /// </remarks>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TelegramUser>(entity =>
            {
                entity.Property(e => e.Id).IsRequired();
                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<TelegramUser>().UseXminAsConcurrencyToken();
        }
    }
}

/*
 * DB Entity Framework Migration script
 *
 * dotnet tool install --global dotnet-ef / dotnet tool update --global dotnet-ef
 * dotnet ef migrations add KIP_DB_Migration
 * dotnet ef database update
 */
