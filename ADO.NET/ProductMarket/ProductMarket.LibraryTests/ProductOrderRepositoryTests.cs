using Moq;
using System.Data;
using System.Threading.Tasks;
using ProductMarket.Libary.Data;
using ProductMarket.Libary.Models;
using Xunit;
using FluentAssertions;

namespace ProductMarket.Library.Tests
{
    public class ProductOrderRepositoryTests
    {
        private readonly Mock<IDbConnectionFactory> _connectionFactoryMock;
        private readonly Mock<IDbConnection> _connectionMock;
        private readonly Mock<IDbCommand> _commandMock;
        private readonly Mock<IDataReader> _readerMock;
        private readonly ProductOrderRepository _repository;

        public ProductOrderRepositoryTests()
        {
            _connectionFactoryMock = new Mock<IDbConnectionFactory>();
            _connectionMock = new Mock<IDbConnection>();
            _commandMock = new Mock<IDbCommand>();
            _readerMock = new Mock<IDataReader>();

            _connectionFactoryMock.Setup(x => x.CreateConnection()).Returns(_connectionMock.Object);
            _repository = new ProductOrderRepository(_connectionFactoryMock.Object);

            // Set up the IDbConnection mock
            _connectionMock.Setup(conn => conn.State).Returns(ConnectionState.Open);
            _connectionMock.Setup(conn => conn.CreateCommand()).Returns(_commandMock.Object);

            // Set up the IDbCommand mock for synchronous method
            _commandMock.Setup(cmd => cmd.ExecuteReader()).Returns(_readerMock.Object);
        }

        [Fact]
        public async Task GetAllProductsAsync_ShouldReturnListOfProducts_WhenProductsExist()
        {
            // Arrange
            var products = new List<Product>
            {
                new Product { Id = 1, Name = "Product 1", Description = "Description 1", Weight = 1.0m, Height = 1.0m, Width = 1.0m, Length = 1.0m },
                new Product { Id = 2, Name = "Product 2", Description = "Description 2", Weight = 2.0m, Height = 2.0m, Width = 2.0m, Length = 2.0m }
            };

            _commandMock.Setup(cmd => cmd.CommandText).Returns("SELECT * FROM Products");
            _readerMock.SetupSequence(r => r.Read())
                .Returns(true)
                .Returns(true)
                .Returns(false);

            _readerMock.SetupSequence(r => r.GetInt32(0))
                .Returns(products[0].Id)
                .Returns(products[1].Id);

            _readerMock.SetupSequence(r => r.GetString(1))
                .Returns(products[0].Name)
                .Returns(products[1].Name);

            // Act
            var result = await Task.Run(() => _repository.GetAllProductsAsync());

            // Assert
            result.Should().BeEquivalentTo(products);
        }

        [Fact]
        public async Task GetAllOrdersAsync_ShouldReturnListOfOrders_WhenOrdersExist()
        {
            // Arrange
            var orders = new List<Order>
            {
                new Order { Id = 1, Status = "Completed", CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now, ProductId = 1 },
                new Order { Id = 2, Status = "Pending", CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now, ProductId = 2 }
            };

            _commandMock.Setup(cmd => cmd.CommandText).Returns("GetFilteredOrders");
            _commandMock.Setup(cmd => cmd.CommandType).Returns(CommandType.StoredProcedure);
            _readerMock.SetupSequence(r => r.Read())
                .Returns(true)
                .Returns(true)
                .Returns(false);

            _readerMock.SetupSequence(r => r.GetInt32(0))
                .Returns(orders[0].Id)
                .Returns(orders[1].Id);

            _readerMock.SetupSequence(r => r.GetString(1))
                .Returns(orders[0].Status)
                .Returns(orders[1].Status);

            // Act
            var result = await Task.Run(() => _repository.GetAllOrdersAsync());

            // Assert
            result.Should().BeEquivalentTo(orders);
        }
    }
}
