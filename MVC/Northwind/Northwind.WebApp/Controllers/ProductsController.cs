using Microsoft.AspNetCore.Mvc;
using Northwind.WebApp.Data;

namespace Northwind.WebApp.Controllers
{
    public class ProductsController : Controller
    {
        private readonly NorthwindContext _context;

        public ProductsController(NorthwindContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var products = _context.Products
                .GetProductsWithDetails(_context.Categories)
                .ToList();
            return View(products);
        }
    }
}
