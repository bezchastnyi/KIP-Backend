using Microsoft.EntityFrameworkCore;

namespace KIP_server_GET.Models
{
    public class Server_GETContext: DbContext
    {
        public DbSet<Faculty> Faculties { get; set; }

        public Server_GETContext(DbContextOptions<Server_GETContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
