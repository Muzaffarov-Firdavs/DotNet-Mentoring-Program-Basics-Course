using Dapper;
using ProductMarket.DapperLib.Models;

namespace ProductMarket.DapperLib.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IDapperRepository<Product> _dapperRepository;

        public ProductRepository(IDapperRepository<Product> dapperRepository)
        {
            _dapperRepository = dapperRepository;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            var query = "SELECT * FROM Products";
            return await _dapperRepository.SelectAllAsync(query);
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            var query = "SELECT * FROM Products WHERE Id = @Id";
            var parameters = new DynamicParameters();
            parameters.Add("@Id", id);
            return await _dapperRepository.SelectAsync(query, parameters);
        }

        public async Task CreateProductAsync(Product product)
        {
            var query = @"INSERT INTO Products (Name, Description, Weight, Height, Width, Length) OUTPUT INSERTED.Id VALUES (@Name, @Description, @Weight, @Height, @Width, @Length)";
            var parameters = new DynamicParameters(new
            {
                product.Name,
                product.Description,
                product.Weight,
                product.Height,
                product.Width,
                product.Length
            });
            product.Id = await _dapperRepository.ExecuteInsertAsync(query, parameters);
        }

        public async Task UpdateProductAsync(Product product)
        {
            var query = @"UPDATE Products SET Name = @Name, Description = @Description, Weight = @Weight, Height = @Height, Width = @Width, Length = @Length WHERE Id = @Id";
            var parameters = new DynamicParameters(product);
            await _dapperRepository.UpdateAsync(query, parameters);
        }

        public async Task DeleteProductAsync(int id)
        {
            var query = "DELETE FROM Products WHERE Id = @Id";
            var parameters = new DynamicParameters();
            parameters.Add("@Id", id);
            await _dapperRepository.DeleteAsync(query, parameters);
        }
    }
}
