using Newtonsoft.Json;

class Program
{
    static async Task Main(string[] args)
    {
        var baseAddress = "https://localhost:7076/api/";
        using var httpClient = new HttpClient { BaseAddress = new Uri(baseAddress) };

        // Fetch categories
        var categoriesResponse = await httpClient.GetStringAsync("categories");
        var categories = JsonConvert.DeserializeObject<dynamic>(categoriesResponse);
        Console.WriteLine("Categories:");
        Console.WriteLine(categories);

        // Fetch products
        var productsResponse = await httpClient.GetStringAsync("products?pageNumber=1&pageSize=10");
        var products = JsonConvert.DeserializeObject<dynamic>(productsResponse);
        Console.WriteLine("Products:");
        Console.WriteLine(products);

        // Example of creating a product
        var newProduct = new
        {
            ProductName = "Sample Product",
            CategoryId = 1,
            UnitPrice = 10.99m,
            UnitsInStock = 100
        };
        var content = new StringContent(JsonConvert.SerializeObject(newProduct), System.Text.Encoding.UTF8, "application/json");
        var postResponse = await httpClient.PostAsync("products", content);
        Console.WriteLine("Create Product Status: " + postResponse.StatusCode);
    }
}
