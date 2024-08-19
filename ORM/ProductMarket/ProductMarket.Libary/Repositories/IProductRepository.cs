using ProductMarket.Libary.Models;

namespace ProductMarket.Libary.Repositories
{
    public interface IProductRepository
    {
        Task AddProductAsync(Product product);
        Task DeleteProductAsync(int id);
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product?> GetProductByIdAsync(int id);
        Task UpdateProductAsync(Product product);
    }
}