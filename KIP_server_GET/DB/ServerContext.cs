using KIP_server_GET.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace KIP_server_GET.DB
{
    public class ServerContext: DbContext
    {
        public DbSet<Faculty> Faculty { get; set; }

        public ServerContext()
        {
            //Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=KIP_database;User Id=postgres;Password=KIP");
        }
    }
}
