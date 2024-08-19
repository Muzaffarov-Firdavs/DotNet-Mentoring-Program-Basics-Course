using ProductMarket.Libary.Models;

namespace ProductMarket.Libary.Repositories
{
    public interface IOrderRepository
    {
        Task AddOrderAsync(Order order);
        Task BulkDeleteOrdersAsync(int? year, int? month, OrderStatus? status, int? productId);
        Task DeleteOrderAsync(int id);
        Task<IEnumerable<Order>> GetOrdersAsync(int? year, int? month, OrderStatus? status, int? productId);
        Task UpdateOrderAsync(Order order);
    }
}