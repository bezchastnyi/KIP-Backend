using KIP_server_TB.Models;
using Microsoft.EntityFrameworkCore;

namespace KIP_server_TB.DB
{
    /// <summary>
    /// Telegram Api Db context.
    /// </summary>
    public class TelegramApiDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TelegramApiDbContext"/> class.
        /// </summary>
        /// <param name="options">DB context options.</param>
        public TelegramApiDbContext(DbContextOptions<TelegramApiDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Gets or sets the audience.
        /// </summary>
        public virtual DbSet<TelegramUser> Users { get; set; }

        /// <inheritdoc/>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TelegramUser>(entity =>
            {
                entity.HasKey(e => e.UserId);
                entity.Property(e => e.UserId).IsRequired();

                entity.Property(e => e.UserName).HasDefaultValue(null);
                entity.Property(e => e.UserEmail).HasDefaultValue(null);
                entity.Property(e => e.UserPassword).HasDefaultValue(null);
                entity.Property(e => e.FacultyId).HasDefaultValue(null);
                entity.Property(e => e.FacultyName).HasDefaultValue(null);
                entity.Property(e => e.FacultyShortName).HasDefaultValue(null);
                entity.Property(e => e.Course).HasDefaultValue(null);
                entity.Property(e => e.GroupId).HasDefaultValue(null);
                entity.Property(e => e.GroupName).HasDefaultValue(null);
                entity.Property(e => e.TempProfValue).HasDefaultValue(null);
                entity.Property(e => e.TempBuildingValue).HasDefaultValue(null);
                entity.Property(e => e.TempAudienceValue).HasDefaultValue(null);
                entity.Property(e => e.TempDayValue).HasDefaultValue(null);

                entity.HasIndex(e => e.UserId).IsUnique();
                entity.HasIndex(e => e.UserEmail).IsUnique();

                entity.UseXminAsConcurrencyToken();
            });
        }
    }
}

/*
 * DB Entity Framework Migration script
 *
 * dotnet tool install --global dotnet-ef / dotnet tool update --global dotnet-ef
 * dotnet ef migrations add KIP_DB_TelegramApi_Migration
 * dotnet ef database update
 */
