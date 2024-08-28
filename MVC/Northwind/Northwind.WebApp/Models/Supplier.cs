namespace Northwind.WebApp.Models
{
    public class Supplier
    {
        public int SupplierID { get; set; }
        public string ContactName { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
