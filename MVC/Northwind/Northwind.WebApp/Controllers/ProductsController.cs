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
            var products = _context.Products
                .GetProductsWithDetails(_context.Categories)
                .ToList();

            return View(products);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            ViewData["CategoryID"] = new SelectList(await _context.Categories.ToListAsync(), "CategoryID", "CategoryName", product.CategoryID);
            ViewData["SupplierID"] = new SelectList(await _context.Suppliers.ToListAsync(), "SupplierID", "SupplierName", product.SupplierID);

            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductID,ProductName,SupplierID,CategoryID,QuantityPerUnit,UnitPrice,UnitsInStock,UnitsOnOrder,ReorderLevel,Discontinued")] Product product)
        {
            if (id != product.ProductID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["CategoryID"] = new SelectList(await _context.Categories.ToListAsync(), "CategoryID", "CategoryName", product.CategoryID);
            ViewData["SupplierID"] = new SelectList(await _context.Suppliers.ToListAsync(), "SupplierID", "SupplierName", product.SupplierID);

            return View(product);
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ProductID == id);
        }
    }
}
