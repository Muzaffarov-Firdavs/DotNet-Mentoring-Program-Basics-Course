using Dapper;
using Moq;
using ProductMarket.DapperLib.Models;
using ProductMarket.DapperLib.Repositories;
using System.Data;

namespace ProductMarket.DapperLibTests.Repositories
{
    public class OrderRepositoryTests
    {
        private readonly Mock<IDapperRepository<Order>> _mockDapperRepository;
        private readonly OrderRepository _orderRepository;

        public OrderRepositoryTests()
        {
            _mockDapperRepository = new Mock<IDapperRepository<Order>>();
            _orderRepository = new OrderRepository(_mockDapperRepository.Object);
        }

        [Fact]
        public async Task CreateOrderAsync_ShouldCallInsertAsync_WithCorrectQueryAndParameters()
        {
            // Arrange
            var order = new Order
            {
                Status = "New",
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow,
                ProductId = 1
            };

            var expectedQuery = @"INSERT INTO Orders (Status, CreatedDate, UpdatedDate, ProductId) OUTPUT INSERTED.Id VALUES (@Status, @CreatedDate, @UpdatedDate, @ProductId)";
            _mockDapperRepository
                .Setup(repo => repo.ExecuteInsertAsync(It.Is<string>(q => q == expectedQuery),
                                                       It.IsAny<DynamicParameters>(),
                                                       CommandType.Text))
                .ReturnsAsync(1); 

            // Act
            await _orderRepository.CreateOrderAsync(order);

            // Assert
            _mockDapperRepository.Verify(repo =>
                repo.ExecuteInsertAsync(It.Is<string>(q => q == expectedQuery),
                                        It.IsAny<DynamicParameters>(),
                                        CommandType.Text),
                Times.Once);

            Assert.Equal(1, order.Id);
        }

        [Fact]
        public async Task GetAllOrdersAsync_ShouldCallSelectAllAsync_AndReturnOrders()
        {
            // Arrange
            var expectedOrders = new List<Order>
            {
                new Order { Id = 1, Status = "New", CreatedDate = DateTime.UtcNow, UpdatedDate = DateTime.UtcNow, ProductId = 1 },
                new Order { Id = 2, Status = "Completed", CreatedDate = DateTime.UtcNow, UpdatedDate = DateTime.UtcNow, ProductId = 2 }
            };

            var expectedQuery = "GetFilteredOrders";

            _mockDapperRepository
                .Setup(repo => repo.SelectAllAsync(expectedQuery, It.IsAny<DynamicParameters>(), CommandType.StoredProcedure))
                .ReturnsAsync(expectedOrders);

            // Act
            var actualOrders = await _orderRepository.GetAllOrdersAsync();

            // Assert
            _mockDapperRepository.Verify(repo =>
                repo.SelectAllAsync(It.Is<string>(q => q == expectedQuery),
                                    It.IsAny<DynamicParameters>(),
                                    CommandType.StoredProcedure),
                Times.Once);

            Assert.Equal(expectedOrders.Count, actualOrders.Count());

            foreach (var order in expectedOrders)
            {
                Assert.Contains(actualOrders, o => o.Id == order.Id &&
                                                    o.Status == order.Status &&
                                                    o.CreatedDate == order.CreatedDate &&
                                                    o.UpdatedDate == order.UpdatedDate &&
                                                    o.ProductId == order.ProductId);
            }
        }

        [Fact]
        public async Task GetOrderByIdAsync_ShouldCallSelectAsync_WithCorrectQueryAndParameters()
        {
            // Arrange
            var orderId = 1;
            var expectedOrder = new Order
            {
                Id = orderId,
                Status = "New",
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow,
                ProductId = 1
            };

            var expectedQuery = "SELECT * FROM Orders WHERE Id = @Id";
            var expectedParameters = new DynamicParameters();
            expectedParameters.Add("@Id", orderId);

            _mockDapperRepository
                .Setup(repo => repo.SelectAsync(It.Is<string>(q => q == expectedQuery),
                                                It.Is<DynamicParameters>(p => p.Get<int>("@Id") == orderId),
                                                CommandType.Text))
                .ReturnsAsync(expectedOrder);

            // Act
            var actualOrder = await _orderRepository.GetOrderByIdAsync(orderId);

            // Assert
            _mockDapperRepository.Verify(repo =>
                repo.SelectAsync(It.Is<string>(q => q == expectedQuery),
                                 It.Is<DynamicParameters>(p => p.Get<int>("@Id") == orderId),
                                 CommandType.Text),
                Times.Once);

            Assert.NotNull(actualOrder);
            Assert.Equal(expectedOrder.Id, actualOrder.Id);
            Assert.Equal(expectedOrder.Status, actualOrder.Status);
            Assert.Equal(expectedOrder.CreatedDate, actualOrder.CreatedDate);
            Assert.Equal(expectedOrder.UpdatedDate, actualOrder.UpdatedDate);
            Assert.Equal(expectedOrder.ProductId, actualOrder.ProductId);
        }

        [Fact]
        public async Task UpdateOrderAsync_ShouldCallUpdateAsync_WithCorrectQueryAndParameters()
        {
            // Arrange
            var order = new Order
            {
                Id = 1,
                Status = "Updated",
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow,
                ProductId = 1
            };

            var expectedQuery = @"UPDATE Orders SET Status = @Status, CreatedDate = @CreatedDate, UpdatedDate = @UpdatedDate, ProductId = @ProductId WHERE Id = @Id";

            _mockDapperRepository
                .Setup(repo => repo.UpdateAsync(It.Is<string>(q => q == expectedQuery),
                                                It.IsAny<DynamicParameters>(),
                                                CommandType.Text))
                .Returns(Task.CompletedTask);

            // Act
            await _orderRepository.UpdateOrderAsync(order);

            // Assert
            _mockDapperRepository.Verify(repo =>
                repo.UpdateAsync(It.Is<string>(q => q == expectedQuery),
                                 It.IsAny<DynamicParameters>(),
                                 CommandType.Text),
                Times.Once);
        }

        [Fact]
        public async Task DeleteOrderAsync_ShouldCallDeleteAsync_WithCorrectQueryAndParameters()
        {
            // Arrange
            int orderId = 1;
            var expectedQuery = "DELETE FROM Orders WHERE Id = @Id";

            _mockDapperRepository
                .Setup(repo => repo.DeleteAsync(It.Is<string>(q => q == expectedQuery),
                                                It.Is<DynamicParameters>(p => p.Get<int>("@Id") == orderId),
                                                CommandType.Text))
                .Returns(Task.CompletedTask);

            // Act
            await _orderRepository.DeleteOrderAsync(orderId);

            // Assert
            _mockDapperRepository.Verify(repo =>
                repo.DeleteAsync(It.Is<string>(q => q == expectedQuery),
                                 It.Is<DynamicParameters>(p => p.Get<int>("@Id") == orderId),
                                 CommandType.Text),
                Times.Once);
        }

        [Fact]
        public async Task DeleteOrdersInBulkAsync_ShouldCallDeleteAsync_ForEachOrder()
        {
            // Arrange
            var ordersToDelete = new List<Order>
            {
                new Order { Id = 1 },
                new Order { Id = 2 }
            };

            var expectedQuery = "DELETE FROM Orders WHERE Id = @Id";

            _mockDapperRepository
                .Setup(repo => repo.SelectAllAsync(It.IsAny<string>(), It.IsAny<DynamicParameters>(), CommandType.StoredProcedure))
                .ReturnsAsync(ordersToDelete);

            _mockDapperRepository
                .Setup(repo => repo.DeleteAsync(It.Is<string>(q => q == expectedQuery),
                                                It.IsAny<DynamicParameters>(),
                                                CommandType.Text))
                .Returns(Task.CompletedTask);

            // Act
            await _orderRepository.DeleteOrdersInBulkAsync();

            // Assert
            _mockDapperRepository.Verify(repo =>
                repo.DeleteAsync(It.Is<string>(q => q == expectedQuery),
                                 It.Is<DynamicParameters>(p => p.Get<int>("@Id") == 1),
                                 CommandType.Text),
                Times.Once);

            _mockDapperRepository.Verify(repo =>
                repo.DeleteAsync(It.Is<string>(q => q == expectedQuery),
                                 It.Is<DynamicParameters>(p => p.Get<int>("@Id") == 2),
                                 CommandType.Text),
                Times.Once);
        }
    }
}
