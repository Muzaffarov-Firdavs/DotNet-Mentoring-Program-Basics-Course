using ProductMarket.Libary.Models;
using System.Data;
using System.Data.SqlClient;

namespace ProductMarket.Libary.Data
{
    public class ProductOrderRepository : IProductOrderRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public ProductOrderRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        #region Product CRUD Operations

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            var products = new List<Product>();

            using (var connection = _connectionFactory.CreateConnection())
            using (var command = new SqlCommand("SELECT * FROM Products", connection as SqlConnection))
            {
                connection.Open();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        products.Add(new Product
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Description = reader.GetString(2),
                            Weight = reader.GetDecimal(3),
                            Height = reader.GetDecimal(4),
                            Width = reader.GetDecimal(5),
                            Length = reader.GetDecimal(6)
                        });
                    }
                }
            }

            return products;
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            Product product = null;

            using (var connection = _connectionFactory.CreateConnection())
            using (var command = new SqlCommand("SELECT * FROM Products WHERE Id = @Id", connection as SqlConnection))
            {
                command.Parameters.AddWithValue("@Id", id);
                connection.Open();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        product = new Product
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Description = reader.GetString(2),
                            Weight = reader.GetDecimal(3),
                            Height = reader.GetDecimal(4),
                            Width = reader.GetDecimal(5),
                            Length = reader.GetDecimal(6)
                        };
                    }
                }
            }

            return product;
        }

        public async Task CreateProductAsync(Product product)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                var sqlConnection = (SqlConnection)connection;
                await sqlConnection.OpenAsync();

                var query = @"INSERT INTO Products (Name, Description, Weight, Height, Width, Length) 
                      OUTPUT INSERTED.Id
                      VALUES (@Name, @Description, @Weight, @Height, @Width, @Length)";

                using (var command = new SqlCommand(query, sqlConnection))
                {
                    command.Parameters.AddWithValue("@Name", product.Name);
                    command.Parameters.AddWithValue("@Description", product.Description);
                    command.Parameters.AddWithValue("@Weight", product.Weight);
                    command.Parameters.AddWithValue("@Height", product.Height);
                    command.Parameters.AddWithValue("@Width", product.Width);
                    command.Parameters.AddWithValue("@Length", product.Length);

                    product.Id = (int)await command.ExecuteScalarAsync();
                }
            }
        }

        public async Task UpdateProductAsync(Product product)
        {
            using (var connection = _connectionFactory.CreateConnection())
            using (var command = new SqlCommand(
                "UPDATE Products SET Name = @Name, Description = @Description, Weight = @Weight, Height = @Height, Width = @Width, Length = @Length WHERE Id = @Id", connection as SqlConnection))
            {
                command.Parameters.AddWithValue("@Id", product.Id);
                command.Parameters.AddWithValue("@Name", product.Name);
                command.Parameters.AddWithValue("@Description", product.Description);
                command.Parameters.AddWithValue("@Weight", product.Weight);
                command.Parameters.AddWithValue("@Height", product.Height);
                command.Parameters.AddWithValue("@Width", product.Width);
                command.Parameters.AddWithValue("@Length", product.Length);

                connection.Open();
                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task DeleteProductAsync(int id)
        {
            using (var connection = _connectionFactory.CreateConnection())
            using (var command = new SqlCommand("DELETE FROM Products WHERE Id = @Id", connection as SqlConnection))
            {
                command.Parameters.AddWithValue("@Id", id);
                connection.Open();
                await command.ExecuteNonQueryAsync();
            }
        }

        #endregion

        #region Order CRUD Operations

        public async Task<IEnumerable<Order>> GetAllOrdersAsync(int? year = null, int? month = null, string status = null, int? productId = null)
        {
            var orders = new List<Order>();

            using (var connection = _connectionFactory.CreateConnection())
            using (var command = new SqlCommand("GetFilteredOrders", connection as SqlConnection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Year", year.HasValue ? (object)year.Value : DBNull.Value);
                command.Parameters.AddWithValue("@Month", month.HasValue ? (object)month.Value : DBNull.Value);
                command.Parameters.AddWithValue("@Status", status ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@ProductId", productId.HasValue ? (object)productId.Value : DBNull.Value);

                connection.Open();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        orders.Add(new Order
                        {
                            Id = reader.GetInt32(0),
                            Status = reader.GetString(1),
                            CreatedDate = reader.GetDateTime(2),
                            UpdatedDate = reader.GetDateTime(3),
                            ProductId = reader.GetInt32(4)
                        });
                    }
                }
            }

            return orders;
        }

        public async Task CreateOrderAsync(Order order)
        {
            using (var connection = _connectionFactory.CreateConnection())
            using (var command = new SqlCommand(
                @"INSERT INTO Orders (Status, CreatedDate, UpdatedDate, ProductId) 
                  OUTPUT INSERTED.Id 
                  VALUES (@Status, @CreatedDate, @UpdatedDate, @ProductId)", connection as SqlConnection))
            {
                command.Parameters.AddWithValue("@Status", order.Status);
                command.Parameters.AddWithValue("@CreatedDate", order.CreatedDate);
                command.Parameters.AddWithValue("@UpdatedDate", order.UpdatedDate);
                command.Parameters.AddWithValue("@ProductId", order.ProductId);

                connection.Open();

                order.Id = (int)await command.ExecuteScalarAsync();
            }
        }

        public async Task UpdateOrderAsync(Order order)
        {
            using (var connection = _connectionFactory.CreateConnection())
            using (var command = new SqlCommand(
                "UPDATE Orders SET Status = @Status, CreatedDate = @CreatedDate, UpdatedDate = @UpdatedDate, ProductId = @ProductId WHERE Id = @Id", connection as SqlConnection))
            {
                command.Parameters.AddWithValue("@Id", order.Id);
                command.Parameters.AddWithValue("@Status", order.Status);
                command.Parameters.AddWithValue("@CreatedDate", order.CreatedDate);
                command.Parameters.AddWithValue("@UpdatedDate", order.UpdatedDate);
                command.Parameters.AddWithValue("@ProductId", order.ProductId);

                connection.Open();
                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task DeleteOrderAsync(int id)
        {
            using (var connection = _connectionFactory.CreateConnection())
            using (var command = new SqlCommand("DELETE FROM Orders WHERE Id = @Id", connection as SqlConnection))
            {
                command.Parameters.AddWithValue("@Id", id);
                connection.Open();
                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task DeleteOrdersInBulkAsync(int? year = null, int? month = null, string status = null, int? productId = null)
        {
            var ordersToDelete = await GetAllOrdersAsync(year, month, status, productId);
            using (var connection = _connectionFactory.CreateConnection())
            {
                connection.Open();
                foreach (var order in ordersToDelete)
                {
                    using (var command = new SqlCommand("DELETE FROM Orders WHERE Id = @Id", connection as SqlConnection))
                    {
                        command.Parameters.AddWithValue("@Id", order.Id);
                        await command.ExecuteNonQueryAsync();
                    }
                }
            }
        }

        #endregion
    }
}
