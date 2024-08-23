using Northwind.WebApp.Models;

namespace Northwind.WebApp.Data
{
    public static class NorthwindContextExtensions
    {
        public static IQueryable<Product> GetProductsWithDetails(this IQueryable<Product> products, IQueryable<Category> categories)
        {
            return from product in products
                   join category in categories on product.CategoryID equals category.CategoryID
                   select new Product
                   {
                       ProductID = product.ProductID,
                       ProductName = product.ProductName,
                       SupplierID = product.SupplierID,
                       CategoryID = product.CategoryID,
                       QuantityPerUnit = product.QuantityPerUnit,
                       UnitPrice = product.UnitPrice,
                       UnitsInStock = product.UnitsInStock,
                       UnitsOnOrder = product.UnitsOnOrder,
                       ReorderLevel = product.ReorderLevel,
                       Discontinued = product.Discontinued,
                       CategoryName = category.CategoryName,
                       SupplierName = ""
                   };
        }
    }
}
