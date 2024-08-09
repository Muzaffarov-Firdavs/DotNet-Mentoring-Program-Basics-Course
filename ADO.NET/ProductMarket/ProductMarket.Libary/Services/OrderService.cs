using Microsoft.EntityFrameworkCore;

namespace ProductMarket.Libary.Services
{
    public class OrderService : IOrderService
    {
        private readonly Data.ProductOrderContext _context;

        public OrderService(Data.ProductOrderContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Models.Order>> GetAllOrdersAsync(
            int? year = null, int? month = null, string status = null, int? productId = null)
        {
            var query = _context.Orders.AsQueryable();

            if (year.HasValue)
                query = query.Where(o => o.CreatedDate.Year == year.Value);

            if (month.HasValue)
                query = query.Where(o => o.CreatedDate.Month == month.Value);

            if (!string.IsNullOrEmpty(status))
                query = query.Where(o => o.Status == status);

            if (productId.HasValue)
                query = query.Where(o => o.ProductId == productId.Value);

            return await query.Include(o => o.Product).ToListAsync();
        }

        public async Task CreateOrderAsync(Models.Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateOrderAsync(Models.Order order)
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

        public async Task DeleteOrdersInBulkAsync(
            int? year = null, int? month = null, string status = null, int? productId = null)
        {
            var ordersToDelete = await GetAllOrdersAsync(year, month, status, productId);
            _context.Orders.RemoveRange(ordersToDelete);
            await _context.SaveChangesAsync();
        }
    }
}
