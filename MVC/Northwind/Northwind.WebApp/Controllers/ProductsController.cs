using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Northwind.WebApp.Data;
using Northwind.WebApp.Models;

namespace Northwind.WebApp.Controllers
{
    public class ProductsController : Controller
    {
        private readonly NorthwindContext _context;
        private readonly int _maxProductsToShow;

        public ProductsController(NorthwindContext context, IConfiguration configuration)
        {
            _context = context;
            _maxProductsToShow = configuration.GetValue<int>("ProductSettings:MaxProductsToShow");
        }

        public async Task<IActionResult> Index()
        {
            List<Product> products;
            if (_maxProductsToShow == 0)
                products = _context.Products
                    .Include(products => products.Category)
                    .Include(products => products.Supplier)
                    .ToList();
            else
                products = _context.Products
                    .Include(products => products.Category)
                    .Include(products => products.Supplier)
                    .Take(_maxProductsToShow)
                    .ToList();

            return View(products);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound();

            ViewBag.Categories = await _context.Categories.ToListAsync();
            ViewBag.Suppliers = await _context.Suppliers.ToListAsync();

            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Product product)
        {
            if (!ModelState.IsValid) return View(product);

            if (id != product.ProductID) return BadRequest("Product Id and URL Id not match.");

            var existProduct = await _context.Products.FindAsync(id);
            if (existProduct == null) return NotFound();

            _context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "products");
        }
    }
}
