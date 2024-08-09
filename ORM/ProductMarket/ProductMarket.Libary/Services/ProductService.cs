using Microsoft.EntityFrameworkCore;

namespace ProductMarket.Libary.Services
{
    public class ProductService : IProductService
    {
        private readonly Data.ProductOrderContext _context;

        public ProductService(Data.ProductOrderContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Models.Product>> GetAllProductsAsync() => await _context.Products.ToListAsync();

        public async Task<Models.Product> GetProductByIdAsync(int id) => await _context.Products.FindAsync(id);

        public async Task CreateProductAsync(Models.Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProductAsync(Models.Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }
    }
}
