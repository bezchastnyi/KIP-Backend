using KIP_POST_APP.Models.KIP;
using Microsoft.EntityFrameworkCore;

namespace KIP_POST_APP.DB
{
    public class ServerContext : DbContext
    {
        public DbSet<Audience> Audience { get; set; }
        public DbSet<Building> Building { get; set; }
        public DbSet<Cathedra> Cathedra { get; set; }
        public DbSet<Faculty> Faculty { get; set; }
        public DbSet<Group> Group { get; set; }
        public DbSet<Prof> Prof { get; set; }
        public DbSet<ProfSchedule> ProfSchedule { get; set; }
        public DbSet<StudentSchedule> StudentSchedule { get; set; }

        public ServerContext(DbContextOptions<ServerContext> options) : base(options) { }
    }
}

/*
 * DB Entity Framework Migration script
 * 
 * dotnet tool install --global dotnet-ef / dotnet tool update --global dotnet-ef
 * dotnet ef migrations add KIP_DB_Migration
 * dotnet ef database update
 */