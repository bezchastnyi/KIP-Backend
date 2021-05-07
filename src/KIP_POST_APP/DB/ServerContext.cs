using KIP_POST_APP.Models.KIP;
using Microsoft.EntityFrameworkCore;

namespace KIP_POST_APP.DB
{
    /// <summary>
    /// Server context.
    /// </summary>
    public class ServerContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServerContext"/> class.
        /// </summary>
        /// <param name="options">DB context options.</param>
        public ServerContext(DbContextOptions<ServerContext> options)
            : base(options) { }

        /// <summary>
        /// Gets or sets the audience.
        /// </summary>
        /// <value>Audience.</value>
        public DbSet<Audience> Audience { get; set; }

        /// <summary>
        /// Gets or sets the building.
        /// </summary>
        /// <value>Building.</value>
        public DbSet<Building> Building { get; set; }

        /// <summary>
        /// Gets or sets the department.
        /// </summary>
        /// <value>Department.</value>
        public DbSet<Cathedra> Cathedra { get; set; }

        /// <summary>
        /// Gets or sets the faculty.
        /// </summary>
        /// <value>Faculty.</value>
        public DbSet<Faculty> Faculty { get; set; }

        /// <summary>
        /// Gets or sets the group.
        /// </summary>
        /// <value>Group.</value>
        public DbSet<Group> Group { get; set; }

        /// <summary>
        /// Gets or sets the teacher.
        /// </summary>
        /// <value>Teacher.</value>
        public DbSet<Prof> Prof { get; set; }

        /// <summary>
        /// Gets or sets the schedule by group.
        /// </summary>
        /// <value>Schedule by group.</value>
        public DbSet<StudentSchedule> StudentSchedule { get; set; }

        /// <summary>
        /// Gets or sets the schedule by teacher.
        /// </summary>
        /// <value>Schedule by teacher.</value>
        public DbSet<ProfSchedule> ProfSchedule { get; set; }

        /// <summary>
        /// Gets or sets the schedule by audience.
        /// </summary>
        /// <value>Schedule by audience.</value>
        public DbSet<AudienceSchedule> AudienceSchedule { get; set; }
    }
}

/*
 * DB Entity Framework Migration script
 *
 * dotnet tool install --global dotnet-ef / dotnet tool update --global dotnet-ef
 * dotnet ef migrations add KIP_DB_Migration
 * dotnet ef database update
 */
