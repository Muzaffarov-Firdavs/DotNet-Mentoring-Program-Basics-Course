using Microsoft.EntityFrameworkCore;
using ProductMarket.Libary.Models;

namespace ProductMarket.Libary.Data
{
    public class ProductOrderContext : DbContext
    {
        public ProductOrderContext(DbContextOptions<ProductOrderContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Product)
                .WithMany()
                .HasForeignKey(o => o.ProductId);
        }
    }
}
