using Dapper;
using Moq;
using ProductMarket.DapperLib.Models;
using ProductMarket.DapperLib.Repositories;
using System.Data;

namespace ProductMarket.DapperLibTests.Repositories
{
    public class ProductRepositoryTests
    {
        private readonly Mock<IDapperRepository<Product>> _mockDapperRepository;
        private readonly ProductRepository _productRepository;

        public ProductRepositoryTests()
        {
            _mockDapperRepository = new Mock<IDapperRepository<Product>>();
            _productRepository = new ProductRepository(_mockDapperRepository.Object);
        }

        [Fact]
        public async Task CreateProductAsync_ShouldCallInsertAsync_WithCorrectQueryAndParameters()
        {
            // Arrange
            var product = new Product
            {
                Name = "Test Product",
                Description = "Test Description",
                Weight = 1.5m,
                Height = 10.0m,
                Width = 5.0m,
                Length = 7.0m
            };

            var expectedQuery = @"INSERT INTO Products (Name, Description, Weight, Height, Width, Length) OUTPUT INSERTED.Id VALUES (@Name, @Description, @Weight, @Height, @Width, @Length)";

            _mockDapperRepository
                .Setup(repo => repo.ExecuteInsertAsync(It.IsAny<string>(), It.IsAny<DynamicParameters>(), CommandType.Text))
                .ReturnsAsync(1);

            // Act
            await _productRepository.CreateProductAsync(product);

            // Assert
            _mockDapperRepository.Verify(repo =>
                repo.ExecuteInsertAsync(It.Is<string>(q => q == expectedQuery),
                                        It.IsAny<DynamicParameters>(),
                                        CommandType.Text),
                Times.Once);

            Assert.Equal(1, product.Id);
        }


        [Fact]
        public async Task GetAllProductsAsync_ShouldCallSelectAllAsync_AndReturnProducts()
        {
            // Arrange
            var expectedProducts = new List<Product>
            {
                new Product { Id = 1, Name = "Product 1", Description = "Description 1", Weight = 1.0m, Height = 10.0m, Width = 5.0m, Length = 7.0m },
                new Product { Id = 2, Name = "Product 2", Description = "Description 2", Weight = 2.0m, Height = 20.0m, Width = 10.0m, Length = 14.0m }
            };

            var expectedQuery = "SELECT * FROM Products";

            _mockDapperRepository
                .Setup(repo => repo.SelectAllAsync(expectedQuery, null, CommandType.Text))
                .ReturnsAsync(expectedProducts);

            // Act
            var actualProducts = await _productRepository.GetAllProductsAsync();

            // Assert
            _mockDapperRepository.Verify(repo =>
                repo.SelectAllAsync(It.Is<string>(q => q == expectedQuery),
                                    null,
                                    CommandType.Text),
                Times.Once);

            Assert.Equal(expectedProducts.Count, actualProducts.Count());

            foreach (var product in expectedProducts)
            {
                Assert.Contains(actualProducts, p => p.Id == product.Id &&
                                                    p.Name == product.Name &&
                                                    p.Description == product.Description &&
                                                    p.Weight == product.Weight &&
                                                    p.Height == product.Height &&
                                                    p.Width == product.Width &&
                                                    p.Length == product.Length);
            }
        }

        [Fact]
        public async Task GetProductByIdAsync_ShouldCallSelectAsync_WithCorrectQueryAndParameters()
        {
            // Arrange
            var productId = 1;
            var expectedProduct = new Product
            {
                Id = productId,
                Name = "Product 1",
                Description = "Description 1",
                Weight = 1.0m,
                Height = 10.0m,
                Width = 5.0m,
                Length = 7.0m
            };

            var expectedQuery = "SELECT * FROM Products WHERE Id = @Id";
            var expectedParameters = new DynamicParameters();
            expectedParameters.Add("@Id", productId);

            _mockDapperRepository
                .Setup(repo => repo.SelectAsync(It.Is<string>(q => q == expectedQuery),
                                                It.Is<DynamicParameters>(p => p.Get<int>("@Id") == productId),
                                                CommandType.Text))
                .ReturnsAsync(expectedProduct);

            // Act
            var actualProduct = await _productRepository.GetProductByIdAsync(productId);

            // Assert
            _mockDapperRepository.Verify(repo =>
                repo.SelectAsync(It.Is<string>(q => q == expectedQuery),
                                 It.Is<DynamicParameters>(p => p.Get<int>("@Id") == productId),
                                 CommandType.Text),
                Times.Once);

            Assert.NotNull(actualProduct);
            Assert.Equal(expectedProduct.Id, actualProduct.Id);
            Assert.Equal(expectedProduct.Name, actualProduct.Name);
            Assert.Equal(expectedProduct.Description, actualProduct.Description);
            Assert.Equal(expectedProduct.Weight, actualProduct.Weight);
            Assert.Equal(expectedProduct.Height, actualProduct.Height);
            Assert.Equal(expectedProduct.Width, actualProduct.Width);
            Assert.Equal(expectedProduct.Length, actualProduct.Length);
        }

        [Fact]
        public async Task UpdateProductAsync_ShouldCallUpdateAsync_WithCorrectQueryAndParameters()
        {
            // Arrange
            var product = new Product
            {
                Id = 1,
                Name = "Updated Product",
                Description = "Updated Description",
                Weight = 2.5m,
                Height = 15.0m,
                Width = 8.0m,
                Length = 10.0m
            };

            var expectedQuery = @"UPDATE Products SET Name = @Name, Description = @Description, Weight = @Weight, Height = @Height, Width = @Width, Length = @Length WHERE Id = @Id";

            _mockDapperRepository
                .Setup(repo => repo.UpdateAsync(It.Is<string>(q => q == expectedQuery),
                                                It.IsAny<DynamicParameters>(),
                                                CommandType.Text))
                .Returns(Task.CompletedTask);

            // Act
            await _productRepository.UpdateProductAsync(product);

            // Assert
            _mockDapperRepository.Verify(repo =>
                repo.UpdateAsync(It.Is<string>(q => q == expectedQuery),
                                 It.IsAny<DynamicParameters>(),
                                 CommandType.Text),
                Times.Once);
        }

        [Fact]
        public async Task DeleteProductAsync_ShouldCallDeleteAsync_WithCorrectQueryAndParameters()
        {
            // Arrange
            int productId = 1;
            var expectedQuery = "DELETE FROM Products WHERE Id = @Id";

            _mockDapperRepository
            .Setup(repo => repo.DeleteAsync(It.Is<string>(q => q == expectedQuery),
                                            It.Is<DynamicParameters>(p => p.Get<int>("@Id") == productId),
                                            CommandType.Text))
            .Returns(Task.CompletedTask);

            // Act
            await _productRepository.DeleteProductAsync(productId);

            // Assert
            _mockDapperRepository.Verify(repo =>
                repo.DeleteAsync(It.Is<string>(q => q == expectedQuery),
                                 It.Is<DynamicParameters>(p => p.Get<int>("@Id") == productId),
                                 CommandType.Text),
                Times.Once);
        }
    }
}
