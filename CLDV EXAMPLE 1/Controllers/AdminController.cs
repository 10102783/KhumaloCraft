using KhumaloCraft.Data;
using KhumaloCraft.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
       
        public IActionResult AdminPage() 
        {
            return View();
        }
        [HttpGet]
        public IActionResult AddProduct() 
        {
            return View(new Product());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddProduct(Product product) 
        {
            if(ModelState.IsValid) 
            {

                _context.Product.Add(product);
                _context.SaveChanges();
                return RedirectToAction("AdminPage");
            }
            return View(product);
        }

    }
}
