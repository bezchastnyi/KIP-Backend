using KIP_Backend.Models.KIP;
using KIP_Backend.Models.UI;
using Microsoft.EntityFrameworkCore;

namespace KIP_Backend.DB
{
    /// <summary>
    /// KIP Db context.
    /// </summary>
    public class KIPDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="KIPDbContext"/> class.
        /// </summary>
        /// <param name="options">DB context options.</param>
        public KIPDbContext(DbContextOptions<KIPDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Gets or sets the audience.
        /// </summary>
        public virtual DbSet<Audience> Audience { get; set; }

        /// <summary>
        /// Gets or sets the building.
        /// </summary>
        public virtual DbSet<Building> Building { get; set; }

        /// <summary>
        /// Gets or sets the department.
        /// </summary>
        public virtual DbSet<Cathedra> Cathedra { get; set; }

        /// <summary>
        /// Gets or sets the faculty.
        /// </summary>
        public virtual DbSet<Faculty> Faculty { get; set; }

        /// <summary>
        /// Gets or sets the group.
        /// </summary>
        public virtual DbSet<Group> Group { get; set; }

        /// <summary>
        /// Gets or sets the teacher.
        /// </summary>
        public virtual DbSet<Prof> Prof { get; set; }

        /// <summary>
        /// Gets or sets the schedule by group.
        /// </summary>
        public virtual DbSet<StudentSchedule> StudentSchedule { get; set; }

        /// <summary>
        /// Gets or sets the schedule by teacher.
        /// </summary>
        public virtual DbSet<ProfSchedule> ProfSchedule { get; set; }

        /// <summary>
        /// Gets or sets the schedule by audience.
        /// </summary>
        public virtual DbSet<AudienceSchedule> AudienceSchedule { get; set; }

        /// <summary>
        /// Gets or sets the audience.
        /// </summary>
        public virtual DbSet<TelegramUser> Users { get; set; }

        /// <inheritdoc/>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Audience>(entity =>
            {
                entity.HasKey(e => e.AudienceID);
                entity.Property(e => e.AudienceID).IsRequired();

                entity.Property(e => e.BuildingID).IsRequired();
                entity.HasOne<Building>()
                      .WithMany()
                      .HasForeignKey(e => e.BuildingID);

                entity.Property(e => e.AudienceName).HasColumnType("varchar(100)").IsRequired();
                entity.Property(e => e.NumberOfSeats);
                entity.Property(e => e.ScheduleIsPresent);

                entity.HasIndex(e => e.AudienceID).IsUnique();
                entity.HasIndex(e => e.BuildingID);

                entity.UseXminAsConcurrencyToken();
            });

            modelBuilder.Entity<AudienceSchedule>(entity =>
            {
                entity.HasKey(e => e.AudienceScheduleID);
                entity.Property(e => e.AudienceScheduleID).ValueGeneratedOnAdd().IsRequired();

                entity.Property(e => e.BuildingID);
                entity.HasOne<Building>()
                      .WithMany()
                      .HasForeignKey(e => e.BuildingID);

                entity.Property(e => e.AudienceID).IsRequired();
                entity.HasOne<Audience>()
                      .WithMany()
                      .HasForeignKey(e => e.AudienceID);

                entity.Property(e => e.ProfID);
                entity.HasOne<Prof>()
                      .WithMany()
                      .HasForeignKey(e => e.ProfID);

                entity.Property(e => e.SubjectName).HasColumnType("varchar(200)").IsRequired();
                entity.Property(e => e.Week).IsRequired();
                entity.Property(e => e.Day).IsRequired();
                entity.Property(e => e.Type).HasColumnType("varchar(100)").IsRequired();
                entity.Property(e => e.Number).IsRequired();
                entity.Property(e => e.GroupNames);
                entity.Property(e => e.ProfName);
                entity.Property(e => e.GroupID);

                entity.HasIndex(e => e.AudienceID);
                entity.HasIndex(e => new { e.AudienceID, e.Day });

                entity.UseXminAsConcurrencyToken();
            });

            modelBuilder.Entity<Building>(entity =>
            {
                entity.HasKey(e => e.BuildingID);
                entity.Property(e => e.BuildingID).IsRequired();

                entity.Property(e => e.BuildingName).HasColumnType("varchar(100)").IsRequired();
                entity.Property(e => e.BuildingShortName).HasColumnType("varchar(5)");

                entity.HasIndex(e => e.BuildingID).IsUnique();

                entity.UseXminAsConcurrencyToken();
            });

            modelBuilder.Entity<Cathedra>(entity =>
            {
                entity.HasKey(e => e.CathedraID);
                entity.Property(e => e.CathedraID).IsRequired();

                entity.Property(e => e.FacultyID).IsRequired();
                entity.HasOne<Faculty>()
                      .WithMany()
                      .HasForeignKey(e => e.FacultyID);

                entity.Property(e => e.CathedraName).HasColumnType("varchar(100)").IsRequired();
                entity.Property(e => e.CathedraShortName).HasColumnType("varchar(7)");

                entity.HasIndex(e => e.CathedraID).IsUnique();
                entity.HasIndex(e => e.FacultyID);

                entity.UseXminAsConcurrencyToken();
            });

            modelBuilder.Entity<Faculty>(entity =>
            {
                entity.HasKey(e => e.FacultyID);
                entity.Property(e => e.FacultyID).IsRequired();

                entity.Property(e => e.FacultyName).HasColumnType("varchar(100)").IsRequired();
                entity.Property(e => e.FacultyShortName).HasColumnType("varchar(7)");

                entity.HasIndex(e => e.FacultyID).IsUnique();

                entity.UseXminAsConcurrencyToken();
            });

            modelBuilder.Entity<Group>(entity =>
            {
                entity.HasKey(e => e.GroupID);
                entity.Property(e => e.GroupID).IsRequired();

                entity.Property(e => e.FacultyID).IsRequired();
                entity.HasOne<Faculty>()
                      .WithMany()
                      .HasForeignKey(e => e.FacultyID);

                entity.Property(e => e.GroupName).HasColumnType("varchar(100)").IsRequired();
                entity.Property(e => e.Course).IsRequired();
                entity.Property(e => e.ScheduleIsPresent);

                entity.HasIndex(e => e.GroupID).IsUnique();
                entity.HasIndex(e => e.FacultyID);

                entity.UseXminAsConcurrencyToken();
            });

            modelBuilder.Entity<Prof>(entity =>
            {
                entity.HasKey(e => e.ProfID);
                entity.Property(e => e.ProfID).IsRequired();

                entity.Property(e => e.CathedraID).IsRequired();
                entity.HasOne<Cathedra>()
                      .WithMany()
                      .HasForeignKey(e => e.CathedraID);

                entity.Property(e => e.ProfSurname).HasColumnType("varchar(100)").IsRequired();
                entity.Property(e => e.ProfName).HasColumnType("varchar(100)").IsRequired();
                entity.Property(e => e.ProfPatronymic).HasColumnType("varchar(100)");
                entity.Property(e => e.ScheduleIsPresent);

                entity.HasIndex(e => e.ProfID).IsUnique();
                entity.HasIndex(e => e.CathedraID);

                entity.UseXminAsConcurrencyToken();
            });

            modelBuilder.Entity<ProfSchedule>(entity =>
            {
                entity.HasKey(e => e.ProfScheduleID);
                entity.Property(e => e.ProfScheduleID).ValueGeneratedOnAdd().IsRequired();

                entity.Property(e => e.BuildingID);
                entity.HasOne<Building>()
                      .WithMany()
                      .HasForeignKey(e => e.BuildingID);

                entity.Property(e => e.AudienceID);
                entity.HasOne<Audience>()
                      .WithMany()
                      .HasForeignKey(e => e.AudienceID);

                entity.Property(e => e.ProfID).IsRequired();
                entity.HasOne<Prof>()
                      .WithMany()
                      .HasForeignKey(e => e.ProfID);

                entity.Property(e => e.SubjectName).HasColumnType("varchar(200)").IsRequired();
                entity.Property(e => e.Week).IsRequired();
                entity.Property(e => e.Day).IsRequired();
                entity.Property(e => e.Type).HasColumnType("varchar(100)").IsRequired();
                entity.Property(e => e.Number).IsRequired();
                entity.Property(e => e.GroupNames);
                entity.Property(e => e.AudienceName);
                entity.Property(e => e.GroupID);

                entity.HasIndex(e => e.ProfID);
                entity.HasIndex(e => new { e.ProfID, e.Day });

                entity.UseXminAsConcurrencyToken();
            });

            modelBuilder.Entity<StudentSchedule>(entity =>
            {
                entity.HasKey(e => e.StudentScheduleID);
                entity.Property(e => e.StudentScheduleID).ValueGeneratedOnAdd().IsRequired();

                entity.Property(e => e.BuildingID);
                entity.HasOne<Building>()
                      .WithMany()
                      .HasForeignKey(e => e.BuildingID);

                entity.Property(e => e.AudienceID);
                entity.HasOne<Audience>()
                      .WithMany()
                      .HasForeignKey(e => e.AudienceID);

                entity.Property(e => e.GroupID).IsRequired();
                entity.HasOne<Group>()
                      .WithMany()
                      .HasForeignKey(e => e.GroupID);

                entity.Property(e => e.ProfID);
                entity.HasOne<Prof>()
                      .WithMany()
                      .HasForeignKey(e => e.ProfID);

                entity.Property(e => e.SubjectName).HasColumnType("varchar(200)").IsRequired();
                entity.Property(e => e.Week).IsRequired();
                entity.Property(e => e.Day).IsRequired();
                entity.Property(e => e.Type).HasColumnType("varchar(100)").IsRequired();
                entity.Property(e => e.Number).IsRequired();
                entity.Property(e => e.ProfName);
                entity.Property(e => e.AudienceName);

                entity.HasIndex(e => e.GroupID);
                entity.HasIndex(e => new { e.GroupID, e.Day });

                entity.UseXminAsConcurrencyToken();
            });

            modelBuilder.Entity<TelegramUser>(entity =>
            {
                entity.HasKey(e => e.UserId);
                entity.Property(e => e.UserId).IsRequired();

                entity.Property(e => e.UserName);
                entity.Property(e => e.Faculty);
                entity.Property(e => e.Course);
                entity.Property(e => e.Group);
                entity.Property(e => e.TempProfValue).HasDefaultValue(null);
                entity.Property(e => e.TempBuildingValue).HasDefaultValue(null);
                entity.Property(e => e.TempAudienceValue).HasDefaultValue(null);
                entity.Property(e => e.TempDayValue).HasDefaultValue(null);

                entity.HasIndex(e => e.UserId).IsUnique();

                entity.UseXminAsConcurrencyToken();
            });
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
