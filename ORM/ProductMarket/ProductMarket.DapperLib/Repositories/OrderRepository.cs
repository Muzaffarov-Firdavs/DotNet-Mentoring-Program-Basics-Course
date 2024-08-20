using Dapper;
using ProductMarket.DapperLib.Models;
using System.Data;

namespace ProductMarket.DapperLib.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IDapperRepository<Order> _dapperRepository;

        public OrderRepository(DapperRepository<Order> dapperRepository)
        {
            _dapperRepository = dapperRepository;
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync(int? year = null, int? month = null, string status = null, int? productId = null)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Year", year);
            parameters.Add("@Month", month);
            parameters.Add("@Status", status);
            parameters.Add("@ProductId", productId);

            return await _dapperRepository.SelectAllAsync("GetFilteredOrders", parameters, CommandType.StoredProcedure);
        }

        public async Task<Order> GetOrderByIdAsync(int id)
        {
            var query = "SELECT * FROM Orders WHERE Id = @Id";
            var parameters = new DynamicParameters();
            parameters.Add("@Id", id);
            return await _dapperRepository.SelectAsync(query, parameters);
        }

        public async Task CreateOrderAsync(Order order)
        {
            var query = @"INSERT INTO Orders (Status, CreatedDate, UpdatedDate, ProductId) 
                          OUTPUT INSERTED.Id 
                          VALUES (@Status, @CreatedDate, @UpdatedDate, @ProductId)";
            var parameters = new DynamicParameters(new
            {
                order.Status,
                order.CreatedDate,
                order.UpdatedDate,
                order.ProductId
            });
            order.Id = await _dapperRepository.ExecuteInsertAsync(query, parameters);
        }

        public async Task UpdateOrderAsync(Order order)
        {
            var query = @"UPDATE Orders SET 
                          Status = @Status, 
                          CreatedDate = @CreatedDate, 
                          UpdatedDate = @UpdatedDate, 
                          ProductId = @ProductId 
                          WHERE Id = @Id";
            var parameters = new DynamicParameters(order);
            await _dapperRepository.UpdateAsync(query, parameters);
        }

        public async Task DeleteOrderAsync(int id)
        {
            var query = "DELETE FROM Orders WHERE Id = @Id";
            var parameters = new DynamicParameters();
            parameters.Add("@Id", id);
            await _dapperRepository.DeleteAsync(query, parameters);
        }

        public async Task DeleteOrdersInBulkAsync(int? year = null, int? month = null, string status = null, int? productId = null)
        {
            var ordersToDelete = await GetAllOrdersAsync(year, month, status, productId);
            var query = "DELETE FROM Orders WHERE Id = @Id";

            foreach (var order in ordersToDelete)
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Id", order.Id);
                await _dapperRepository.DeleteAsync(query, parameters);
            }
        }
    }
}
