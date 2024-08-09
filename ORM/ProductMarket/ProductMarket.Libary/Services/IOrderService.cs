using ProductMarket.Libary.Models;

namespace ProductMarket.Libary.Services
{
    public interface IOrderService
    {
        Task CreateOrderAsync(Order order);
        Task DeleteOrderAsync(int id);
        Task DeleteOrdersInBulkAsync(int? year = null, int? month = null, string status = null, int? productId = null);
        Task<IEnumerable<Order>> GetAllOrdersAsync(int? year = null, int? month = null, string status = null, int? productId = null);
        Task UpdateOrderAsync(Order order);
    }
}