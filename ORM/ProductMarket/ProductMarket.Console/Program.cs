using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductMarket.Libary.Data;
using ProductMarket.Libary.Models;
using ProductMarket.Libary.Repositories;

class Program
{
    static async Task Main(string[] args)
    {
    
    }

    private static ServiceProvider ConfigureServices()
    {
        // Create a new ServiceCollection
        var services = new ServiceCollection();

        // Build configuration
        IConfiguration config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        // Add the configuration to the services
        services.AddSingleton(config);

        // Configure DbContext
        services.AddDbContext<ProductOrderContext>(options =>
            options.UseSqlServer(config.GetConnectionString("DefaultConnection")));

        // Register repositories
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();

        // Build the service provider
        return services.BuildServiceProvider();
    }
}
