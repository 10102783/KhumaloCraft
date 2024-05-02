using KhumaloCraft.Data;
using KhumaloCraft.Models;
using KhumaloCraft.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;

namespace KhumaloCraft.Controllers
{
    public class HomeController : Controller
    {
        private readonly ProductContext _context;
        private readonly ProductRepo _productRepo;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ProductContext context, ProductRepo productRepo)
        {
            _context = context;
            _productRepo = productRepo;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult AboutUS()
        {
            return View();
        }


        public async Task<IActionResult> MYWORK()
        {
            var Product = await _context.Product.ToListAsync();
            return View(Product);
        }

        // Action method to handle the GET request for the contact form
        public IActionResult CONTACTUS()
        {
            return View();
        }

        // Action method to handle the POST request for the contact form submission
        [HttpPost]
        public IActionResult SubmitContactForm(CONTACTUS contact)
        {
            if (ModelState.IsValid)
            {
                // Here, you would typically save the contact information to the database
                // For demonstration purposes, let's assume we just return a success message
                return Content("Form submitted successfully!");
            }
            else
            {
                // If model state is not valid, return the view with validation errors
                return View("CONTACTUS", contact);
            }
        }

        // Existing action methods...

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
