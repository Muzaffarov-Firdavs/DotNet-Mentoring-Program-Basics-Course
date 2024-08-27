using Microsoft.EntityFrameworkCore;
using Northwind.WebApp.Models;

namespace Northwind.WebApp.Data
{
    public class NorthwindContext : DbContext
    {
        public NorthwindContext(DbContextOptions<NorthwindContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryID);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Supplier)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.SupplierID);
        }
    }
}
