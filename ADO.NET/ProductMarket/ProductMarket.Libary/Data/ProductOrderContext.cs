using Microsoft.EntityFrameworkCore;

namespace ProductMarket.Libary.Data
{
    public class ProductOrderContext : DbContext
    {
        public ProductOrderContext(DbContextOptions<ProductOrderContext> options) : base(options)
        {
        }

        public DbSet<Models.Product> Products { get; set; }
        public DbSet<Models.Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.Order>()
                .HasOne(o => o.Product)
                .WithMany()
                .HasForeignKey(o => o.ProductId);
        }
    }
}
