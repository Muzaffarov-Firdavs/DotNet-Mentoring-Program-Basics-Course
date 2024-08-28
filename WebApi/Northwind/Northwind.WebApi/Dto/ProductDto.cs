using Northwind.WebApi.Models;

namespace Northwind.WebApi.Dto
{
    public class ProductDto
    {
        public string ProductName { get; set; }
        public int CategoryId { get; set; }
        public decimal UnitPrice { get; set; }
        public Int16 UnitsInStock { get; set; }
    }
}
