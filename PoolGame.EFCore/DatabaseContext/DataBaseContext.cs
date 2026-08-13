using Microsoft.EntityFrameworkCore;
using PoolGame.EFCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoolGame.EFCore.DatabaseContext
{
    public class DataBaseContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Server=localhost;Database=EFCore-test;Trusted_Connection=True;TrustServerCertificate=True;");
        }

        public DbSet<User> Users => Set<User>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity => {
                entity.HasKey(u => u.UserId);

                entity.Property(prop => (prop.Password))
                .IsRequired()
                .HasMaxLength(128);

                entity.Property(prop => prop.Username)
                .IsRequired()
                .HasMaxLength(128);

                entity.HasIndex(u => u.Username)
                .IsUnique();


            });

            
        }
    }
}
