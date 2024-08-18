using ProductMarket.Libary.Models;

namespace ProductMarket.Libary.Data
{
    public interface IProductOrderRepository
    {
        Task CreateOrderAsync(Order order);
        Task CreateProductAsync(Product product);
        Task DeleteOrderAsync(int id);
        Task DeleteOrdersInBulkAsync(int? year = null, int? month = null, string status = null, int? productId = null);
        Task DeleteProductAsync(int id);
        Task<IEnumerable<Order>> GetAllOrdersAsync(int? year = null, int? month = null, string status = null, int? productId = null);
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product> GetProductByIdAsync(int id);
        Task UpdateOrderAsync(Order order);
        Task UpdateProductAsync(Product product);
    }
}