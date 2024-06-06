using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using KhumaloCraft.Models;
using KhumaloCraft.Data;

namespace KhumaloCraft.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ProductContext _context;

        public AdminController(ProductContext context)
        {
            _context = context;
        }

        // Admin dashboard action
        public IActionResult AdminPage()
        {
            return View();
        }

        // Action to view all products
        public IActionResult ViewProduct()
        {
            var products = _context.Product.ToList();
            return View(products);
        }

        // Example of an admin-only action to insert a new product
        [HttpGet]
        public IActionResult AddProduct()
        {
            return View(new Product()); // Return the view with a new instance of Products model
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Product.Add(product);
                _context.SaveChanges();
                return RedirectToAction("AdminPage");
            }
            return View(product); // Return to the view with validation errors
        }

        // Action to edit a product
        [HttpGet]
        public IActionResult EditProduct(int id)
        {
            var product = _context.Product.Find(id);
            if (product == null)
            {
                return NotFound(); // Return 404 if product is not found
            }
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(product).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction(nameof(ViewProduct));
            }
            return View(product); // Return to the view with validation errors
        }

        // Action to delete a product
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteProduct(int id)
        {
            var product = _context.Product.Find(id);
            if (product == null)
            {
                return NotFound(); // Return 404 if product is not found
            }
            _context.Product.Remove(product);
            _context.SaveChanges();
            return RedirectToAction(nameof(ViewProduct));
        }

        // Action to view all orders
        public IActionResult ViewOrders()
        {
            var orders = _context.Order.Include(o => o.User).ToList();
            return View(orders);
        }

        // Action to process an order
        public IActionResult ProcessOrder(int id)
        {
            var order = _context.Order.Find(id);
            if (order == null)
            {
                return NotFound(); // Return 404 if order is not found
            }

            // Process the order (e.g., update status)
            order.Status = OrderStatus.Processed; // Assuming OrderStatus is an enum
            _context.SaveChanges();

            return RedirectToAction(nameof(ViewOrders));
        }

        // Action to update order status
        [HttpPost]
        public IActionResult UpdateOrderStatus(int id, OrderStatus status)
        {
            var order = _context.Order.Find(id);
            if (order == null)
            {
                return NotFound(); // Return 404 if order is not found
            }

            // Update the order status
            order.Status = status;
            _context.SaveChanges();

            return RedirectToAction(nameof(ViewOrders));
        }

        // Other admin-only actions...
    }
}