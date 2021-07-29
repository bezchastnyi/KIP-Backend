using KIP_POST_APP.Models.KIP;
using Microsoft.EntityFrameworkCore;

namespace KIP_POST_APP.DB
{
    /// <summary>
    /// Post app db context.
    /// </summary>
    public class PostDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PostDbContext"/> class.
        /// </summary>
        /// <param name="options">DB context options.</param>
        public PostDbContext(DbContextOptions<PostDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Gets or sets the audience.
        /// </summary>
        /// <value>Audience.</value>
        public virtual DbSet<Audience> Audience { get; set; }

        /// <summary>
        /// Gets or sets the building.
        /// </summary>
        /// <value>Building.</value>
        public virtual DbSet<Building> Building { get; set; }

        /// <summary>
        /// Gets or sets the department.
        /// </summary>
        /// <value>Department.</value>
        public virtual DbSet<Cathedra> Cathedra { get; set; }

        /// <summary>
        /// Gets or sets the faculty.
        /// </summary>
        /// <value>Faculty.</value>
        public virtual DbSet<Faculty> Faculty { get; set; }

        /// <summary>
        /// Gets or sets the group.
        /// </summary>
        /// <value>Group.</value>
        public virtual DbSet<Group> Group { get; set; }

        /// <summary>
        /// Gets or sets the teacher.
        /// </summary>
        /// <value>Teacher.</value>
        public virtual DbSet<Prof> Prof { get; set; }

        /// <summary>
        /// Gets or sets the schedule by group.
        /// </summary>
        /// <value>Schedule by group.</value>
        public virtual DbSet<StudentSchedule> StudentSchedule { get; set; }

        /// <summary>
        /// Gets or sets the schedule by teacher.
        /// </summary>
        /// <value>Schedule by teacher.</value>
        public virtual DbSet<ProfSchedule> ProfSchedule { get; set; }

        /// <summary>
        /// Gets or sets the schedule by audience.
        /// </summary>
        /// <value>Schedule by audience.</value>
        public virtual DbSet<AudienceSchedule> AudienceSchedule { get; set; }

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
            modelBuilder.Entity<Audience>(entity =>
            {
                entity.Property(e => e.AudienceID).IsRequired();
            });

            modelBuilder.Entity<Audience>().UseXminAsConcurrencyToken();
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
