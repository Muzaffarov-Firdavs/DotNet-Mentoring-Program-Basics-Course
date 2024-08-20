using ProductMarket.DapperLib.Models;

namespace ProductMarket.DapperLib.Repositories
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetAllOrdersAsync(int? year = null, int? month = null, string status = null, int? productId = null);
        Task<Order> GetOrderByIdAsync(int id);
        Task CreateOrderAsync(Order order);
        Task UpdateOrderAsync(Order order);
        Task DeleteOrderAsync(int id);
        Task DeleteOrdersInBulkAsync(int? year = null, int? month = null, string status = null, int? productId = null);
    }
}
