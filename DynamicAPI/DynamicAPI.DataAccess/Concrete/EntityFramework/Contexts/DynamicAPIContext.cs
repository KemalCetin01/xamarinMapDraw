using DynamicAPI.Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace DynamicAPI.DataAccess.Concrete.EntityFramework.Contexts
{
    public class DynamicAPIContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           // optionsBuilder.UseNpgsql(@"Server=DESKTOP-OF69QGO;Database=BuildAppYDDB;Trusted_Connection=true");
            optionsBuilder.UseNpgsql("Host = localhost; Database = postgres; Username = postgres; Password = root");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

        public DbSet<Location> Konums { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
