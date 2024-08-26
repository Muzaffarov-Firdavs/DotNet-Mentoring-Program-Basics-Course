using ProductMarket.DapperLib.Data;
using ProductMarket.DapperLib.Models;
using ProductMarket.DapperLib.Repositories;
using System.Data.SqlClient;

class Program
{
    static async Task Main(string[] args)
    {
        // Instantiate repositories
        var sqlConnection = new SqlConnection(DatabseConnection.CONNECTION_STRING);
        var productRepository = new ProductRepository(new DapperRepository<Product>(sqlConnection));
        var orderRepository = new OrderRepository(new DapperRepository<Order>(sqlConnection));

        // requirement 1: Create a new product
        var newProduct = new Product
        {
            Name = "Sofa",
            Description = "This is a sample furniture",
            Weight = 1.5m,
            Height = 10.0m,
            Width = 5.0m,
            Length = 20.0m
        };
        await productRepository.CreateProductAsync(newProduct);
        Console.WriteLine($"Product created successfully with Id: {newProduct.Id}");

        // requirement 2: Fetch all products
        var products = await productRepository.GetAllProductsAsync();
        Console.WriteLine("Products in the database:");
        foreach (var product in products)
        {
            Console.WriteLine($"- {product.Name} ({product.Description})");
        }

        // requirement 3: Create a new order with the correct ProductId
        var newOrder = new Order
        {
            Status = "NotStarted",
            CreatedDate = DateTime.Now,
            UpdatedDate = DateTime.Now,
            ProductId = newProduct.Id // Use the correct ProductId from the inserted product
        };
        await orderRepository.CreateOrderAsync(newOrder);
        Console.WriteLine("Order created successfully.");

        // requirement 4: Fetch all orders with filtering
        var filteredOrders = await orderRepository.GetAllOrdersAsync(year: DateTime.Now.Year, month: DateTime.Now.Month);
        Console.WriteLine("Filtered Orders:");
        foreach (var order in filteredOrders)
        {
            Console.WriteLine($"- Order ID: {order.Id}, Status: {order.Status}");
        }

        // requirement 5: Delete the newly created order
        await orderRepository.DeleteOrderAsync(newOrder.Id);
        Console.WriteLine("Order deleted successfully.");

        // requirement 6: Bulk delete orders with a specific status
        await orderRepository.DeleteOrdersInBulkAsync(status: "Cancelled");
        Console.WriteLine("Bulk deletion completed.");
    }
}
