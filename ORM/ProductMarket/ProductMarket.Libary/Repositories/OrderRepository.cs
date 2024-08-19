using Microsoft.EntityFrameworkCore;
using ProductMarket.Libary.Data;
using ProductMarket.Libary.Models;

namespace ProductMarket.Libary.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ProductOrderContext _context;

        public OrderRepository(ProductOrderContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Order>> GetOrdersAsync(int? year, int? month, OrderStatus? status, int? productId)
        {
            IQueryable<Order> query = _context.Orders.Include(o => o.Product);

            if (year.HasValue)
                query = query.Where(o => o.CreatedDate.Year == year.Value);

            if (month.HasValue)
                query = query.Where(o => o.CreatedDate.Month == month.Value);

            if (status.HasValue)
                query = query.Where(o => o.Status == status.Value);

            if (productId.HasValue)
                query = query.Where(o => o.ProductId == productId.Value);

            return await query.ToListAsync();
        }

        public async Task BulkDeleteOrdersAsync(int? year, int? month, OrderStatus? status, int? productId)
        {
            IQueryable<Order> query = _context.Orders;

            if (year.HasValue)
                query = query.Where(o => o.CreatedDate.Year == year.Value);

            if (month.HasValue)
                query = query.Where(o => o.CreatedDate.Month == month.Value);

            if (status.HasValue)
                query = query.Where(o => o.Status == status.Value);

            if (productId.HasValue)
                query = query.Where(o => o.ProductId == productId.Value);

            _context.Orders.RemoveRange(query);
            await _context.SaveChangesAsync();
        }

        public async Task AddOrderAsync(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateOrderAsync(Order order)
        {
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteOrderAsync(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
            }
        }
    }
}
