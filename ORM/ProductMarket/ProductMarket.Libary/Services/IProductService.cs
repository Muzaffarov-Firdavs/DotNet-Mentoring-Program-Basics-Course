using ProductMarket.Libary.Models;

namespace ProductMarket.Libary.Services
{
    public interface IProductService
    {
        Task CreateProductAsync(Product product);
        Task DeleteProductAsync(int id);
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product> GetProductByIdAsync(int id);
        Task UpdateProductAsync(Product product);
    }
}