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

        /// <inheritdoc/>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TelegramUser>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.Property(e => e.UserId).IsRequired();
                entity.Property(e => e.UserName);
                entity.Property(e => e.Faculty);
                entity.Property(e => e.Course);
                entity.Property(e => e.Group);
                entity.Property(e => e.TempDayValue).HasDefaultValue(null);

                entity.HasIndex(e => e.UserId).IsUnique();
            });

            modelBuilder.Entity<TelegramUser>().UseXminAsConcurrencyToken();
        }
    }
}

/*
 * DB Entity Framework Migration script
 *
 * dotnet tool install --global dotnet-ef / dotnet tool update --global dotnet-ef
 * dotnet ef migrations add KIP_Telegram_Bot_DB_Migration --context=TelegramDbContext
 * dotnet ef database update --context=TelegramDbContext
 */
