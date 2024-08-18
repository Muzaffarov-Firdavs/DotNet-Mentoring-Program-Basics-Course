using ProductMarket.Libary.Data;
using ProductMarket.Libary.Models;

class Program
{
    static async Task Main(string[] args)
    {
        string connectionString = "Server=EPUZTASW010F;Database=ProductMarketDB;MultipleActiveResultSets=true;Trusted_Connection=true;encrypt=false;";
        

        // Create an instance of the repository
        IProductOrderRepository repository = new ProductOrderRepository(connectionString);

        // requirement 1: Create a new product
        var newProduct = new Product
        {
            Name = "Sample Product",
            Description = "This is a sample product",
            Weight = 1.5m,
            Height = 10.0m,
            Width = 5.0m,
            Length = 20.0m
        };
        await repository.CreateProductAsync(newProduct);

        // Ensure the product has been created and its ID is available
        Console.WriteLine($"Product created successfully with Id: {newProduct.Id}");

        // requirement 2: Fetch all products
        var products = await repository.GetAllProductsAsync();
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
        await repository.CreateOrderAsync(newOrder);
        Console.WriteLine("Order created successfully.");

        // requirement 4: Fetch all orders with filtering
        var filteredOrders = await repository.GetAllOrdersAsync(year: DateTime.Now.Year, month: DateTime.Now.Month);
        Console.WriteLine("Filtered Orders:");
        foreach (var order in filteredOrders)
        {
            Console.WriteLine($"- Order ID: {order.Id}, Status: {order.Status}");
        }

        // requirement 5: 
        await repository.DeleteOrderAsync(newOrder.Id);
        Console.WriteLine("Order deleted successfully.");

        // requirement 6: 
        await repository.DeleteOrdersInBulkAsync(status: "Cancelled");
        Console.WriteLine("Bulk deletion completed.");
    }
}
