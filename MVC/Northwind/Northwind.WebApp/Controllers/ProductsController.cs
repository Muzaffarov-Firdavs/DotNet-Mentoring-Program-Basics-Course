using Microsoft.AspNetCore.Mvc;
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

        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = await _context.Categories.ToListAsync();
            ViewBag.Suppliers = await _context.Suppliers.ToListAsync();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductViewModel productViewModel)
        {
            if (!ModelState.IsValid) return View(productViewModel);

            var product = new Product()
            {
                SupplierID = productViewModel.SupplierID,
                CategoryID = productViewModel.CategoryID,
                ProductName = productViewModel.ProductName,
                QuantityPerUnit = productViewModel.QuantityPerUnit,
                UnitPrice = productViewModel.UnitPrice,
                UnitsInStock = productViewModel.UnitsInStock,
                UnitsOnOrder = productViewModel.UnitsOnOrder,
                ReorderLevel = productViewModel.ReorderLevel,
                Discontinued = productViewModel.Discontinued
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "products");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound();

            ViewBag.Categories = await _context.Categories.ToListAsync();
            ViewBag.Suppliers = await _context.Suppliers.ToListAsync();

            return View(ToViewModel(product));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductViewModel productViewModel)
        {
            productViewModel.ProductID = id;
            if (!ModelState.IsValid) return View(productViewModel);

            var existProduct = await _context.Products.FindAsync(id);
            if (existProduct == null) return NotFound();

            existProduct.SupplierID = productViewModel.SupplierID;
            existProduct.CategoryID = productViewModel.CategoryID;
            existProduct.ProductName = productViewModel.ProductName;
            existProduct.QuantityPerUnit = productViewModel.QuantityPerUnit;
            existProduct.UnitPrice = productViewModel.UnitPrice;
            existProduct.UnitsInStock = productViewModel.UnitsInStock;
            existProduct.UnitsOnOrder = productViewModel.UnitsOnOrder;
            existProduct.ReorderLevel = productViewModel.ReorderLevel;
            existProduct.Discontinued = productViewModel.Discontinued;

            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "products");
        }

        private ProductViewModel ToViewModel(Product product)
        {
            return new ProductViewModel()
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
                Discontinued = product.Discontinued
            };
        }
    }
}
