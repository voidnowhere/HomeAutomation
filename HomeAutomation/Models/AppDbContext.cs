using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAutomation.Models
{
    internal class AppDbContext : DbContext
    {
        public DbSet<Person> People { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<AC> ACs { get; set; }
        public DbSet<Heater> Heaters { get; set; }
        public DbSet<TV> TVs { get; set; }
        public DbSet<Door> Doors { get; set; }
        public DbSet<Lamp> Lamps { get; set; }
        public DbSet<Log> Logs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLazyLoadingProxies()
                .UseMySql("Server=localhost;Database=homeautomation;uid=root;password=", new MySqlServerVersion(new Version(8, 0, 30)));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Explicit Discriminator
            modelBuilder.Entity<Person>().HasDiscriminator(p => p.Type);
            modelBuilder.Entity<Equipment>().HasDiscriminator(e => e.Type);
            // Cascade Restrict
            modelBuilder.Entity<Equipment>().HasOne(e => e.Room).WithMany(r => r.Equipments).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Log>().HasOne(l => l.Person).WithMany(p => p.Logs).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Log>().HasOne(l => l.Equipment).WithMany(e => e.Logs).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
