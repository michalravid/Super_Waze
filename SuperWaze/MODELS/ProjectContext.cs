using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;


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
                    "Data Source='your DB name';Initial Catalog=Super-Waze;Integrated Security=SSPI;Trusted_Connection=True;",
                    b => b.MigrationsAssembly("Super Waze"));

            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customers");
                entity.HasKey(e => e.Id_Customer);
                entity.HasMany(c => c.Products)
                  .WithMany(p => p.Customers)
                  .UsingEntity<Dictionary<string, object>>(
                      "CustomerProduct",
                      j => j.HasOne<Product>().WithMany().HasForeignKey("ProductId"),
                      j => j.HasOne<Customer>().WithMany().HasForeignKey("CustomerId")
                  );
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Products");
                entity.HasKey(e => e.Id_Product);
                entity.HasOne(p => p.Shop)
                  .WithMany(s => s.Products)
                  .HasForeignKey(p => p.Id_Shop);
            });

            modelBuilder.Entity<Shop>(entity =>
            {
                entity.ToTable("Shops");
                entity.HasKey(e => e.Id_Shop);
                entity.HasMany(s => s.Customers)
                 .WithMany(c => c.Shops)
                 .UsingEntity<Dictionary<string, object>>(
                     "ShopCustomer",
                     j => j.HasOne<Customer>().WithMany().HasForeignKey("CustomerId"),
                     j => j.HasOne<Shop>().WithMany().HasForeignKey("ShopId")
                 );
            });
        }
    }
}
