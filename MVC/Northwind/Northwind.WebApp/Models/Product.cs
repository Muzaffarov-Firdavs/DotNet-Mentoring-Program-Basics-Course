using System.ComponentModel.DataAnnotations;

namespace Northwind.WebApp.Models
{
    public class Product
    {
        public int ProductID { get; set; }

        [Required(ErrorMessage = "Product name is required.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Product name must be between 3 and 100 characters.")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Supplier is required.")]
        public int SupplierID { get; set; }

        public Supplier Supplier { get; set; }

        [Required(ErrorMessage = "Category is required.")]
        public int CategoryID { get; set; }

        [StringLength(50, ErrorMessage = "Quantity per unit cannot exceed 50 characters.")]

        public Category Category { get; set; }

        public string QuantityPerUnit { get; set; }

        [Range(0.01, 10000, ErrorMessage = "Unit price must be between 0.01 and 10,000.")]
        public decimal UnitPrice { get; set; }

        [Range(0, short.MaxValue, ErrorMessage = "Units in stock must be a non-negative number.")]
        public short UnitsInStock { get; set; }

        [Range(0, short.MaxValue, ErrorMessage = "Units on order must be a non-negative number.")]
        public short UnitsOnOrder { get; set; }

        [Range(0, short.MaxValue, ErrorMessage = "Reorder level must be a non-negative number.")]
        public short ReorderLevel { get; set; }

        public bool Discontinued { get; set; }

        public string SupplierName { get; set; }
        public string CategoryName { get; set; }
    }
}
