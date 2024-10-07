using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MODELS.Models
{
    public class ProjectContext : DbContext
    {
        public ProjectContext()
        {
        }
        public ProjectContext(DbContextOptions<ProjectContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<Shop> Shops { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    "Data Source=localhost\\SQLEXPRESS;Initial Catalog=Super-Waze;Integrated Security=SSPI;Trusted_Connection=True;",
                    b => b.MigrationsAssembly("Super Waze"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer");
                entity.HasKey(c=>c.Id_Customer);
            });
            modelBuilder.Entity<Shop>(entity =>
            {
                entity.ToTable("Shop");
                entity.HasKey(s => s.Id_Shop);
            });
            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");
                entity.HasKey(p=>p.Id_Product);

            });
        }
    }
}
