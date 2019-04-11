using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProAndCate.Models;
using Microsoft.EntityFrameworkCore;

namespace ProAndCate.Controllers
{
    public class HomeController : Controller
    {

        private productContext dbContext;
        public HomeController(productContext context)
        {
            dbContext = context;
            
        }  

        [Route("")]
        [HttpGet("")]
        public IActionResult Index()
        {
            List<Product> ProductList = dbContext.products
            .OrderByDescending(product => product.CreatAt).ToList();
            ViewBag.ProductList = ProductList;
            return View("CreateProduct");
        }

        [HttpGet("products")]
        public IActionResult CreateProduct()
        {
            // show a list of exsiting products
            var ProductList = dbContext.products.OrderByDescending(product => product.CreatAt).ToList();
            ViewBag.ProductList = ProductList;
            return View("CreateProduct");
        }

        [HttpPost("products")]
        public IActionResult CreateProduct(Product product)
        {            
            if (ModelState.IsValid)
            {
                dbContext.Add(product);
                dbContext.SaveChanges();
                return RedirectToAction("CreateProduct");
            }
            else
            {
                List<Product> ProductList = dbContext.products.ToList();
                ViewBag.ProductList = ProductList;
                return View("CreateProduct");            
            }
    
        }


        [HttpGet("categories")]
        public IActionResult CreateCategory()
        {
            // show a list of existing categories
            List<Category> CategoryList = dbContext.categories
            .OrderByDescending(category => category.CreatAt).ToList();
            ViewBag.CategoryList = CategoryList;
            return View("CreateCategory");
        }
        [HttpPost("categories")]
        public IActionResult CreateCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                dbContext.Add(category);
                dbContext.SaveChanges();
                return RedirectToAction("CreateCategory");
            }
            else{
                List<Category> CategoryList = dbContext.categories.ToList();
                ViewBag.CategoryList = CategoryList;
                return View("CreateCategory");
            }
        }

        [HttpGet("products/{productId}")]
        public IActionResult Detail(int productId)
        {
            // show a list of categories that belong to the selected product
            Product Product = dbContext.products.Include(p => p.categories)
            .ThenInclude(a => a.category).FirstOrDefault(p => p.ProductId == productId);
            IEnumerable<Category> UsedCategories = Product.categories
            .Select(a => a.category);
            IEnumerable<Category> UnusedCategories = dbContext.categories
            .Where(cat => !cat.products.Any(a => a.ProductId == productId));
            ViewBag.Product = Product;
            ViewBag.UsedCategories = UsedCategories;
            ViewBag.UnusedCategories = UnusedCategories;
            return View("Detail");
        }

        [HttpPost("products/{productId}")]
        public IActionResult UpdateProduct(int productId, int categoryId)
        {
            // Console.WriteLine("Current Product Id " + productId + " and categoryID " + categoryId);
            Product selectedProduct = dbContext.products.Include(p => p.categories).FirstOrDefault(p => p.ProductId == productId);
            Boolean found = selectedProduct.categories.Any(a => a.CategoryId == categoryId);
            if (!found) {
                Association connection = new Association() 
                {
                    CategoryId = categoryId,
                    ProductId = productId
                };
                selectedProduct.categories.Add(connection);
                dbContext.SaveChanges();
                return RedirectToAction("Detail", new {productId = productId});
            }
            return View("Detail", new {productId = productId});
        }

                [HttpGet("category/{categoryId}")]
        public IActionResult Category(int categoryId) 
        {
            // show a list of products that belongs to the selected categories
            Category Category = dbContext.categories.Include(c => c.products)
                .ThenInclude(a => a.product).FirstOrDefault(c => c.CategoryId == categoryId);
            IEnumerable<Product> UsedProducts = Category.products.Select(a => a.product);
            IEnumerable<Product> UnusedProducts = dbContext.products
                .Where(pro => !pro.categories.Any(a => a.CategoryId == categoryId)); 
            ViewBag.Category = Category;
            ViewBag.UsedProducts = UsedProducts;
            ViewBag.UnusedProducts = UnusedProducts;
            return View("Category");
        }

        [HttpPost("category/{categoryId}")]
        public IActionResult UpdateCategory(int categoryId, int productId)
        {
            Category selectedCategory = dbContext.categories.Include(c => c.products)
                .FirstOrDefault(c => c.CategoryId == categoryId);
            Boolean found = selectedCategory.products.Any(a => a.ProductId == productId);
            if (!found) {
                Association connection = new Association()
                {
                    CategoryId = categoryId,
                    ProductId = productId
                };
                selectedCategory.products.Add(connection);
                dbContext.SaveChanges();
            }
            return RedirectToAction("Category", new {categoryId = categoryId});
        }

    }
}
