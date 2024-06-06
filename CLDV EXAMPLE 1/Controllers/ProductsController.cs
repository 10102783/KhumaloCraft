using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using KhumaloCraft.Data;
using KhumaloCraft.Models;

namespace KhumaloCraft.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ProductContext _context;

        public ProductsController(ProductContext context)
        {
            _context = context;
        }

        // GET: Products/Search
        public async Task<IActionResult> Search(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return View(Enumerable.Empty<Product>());
            }

            var products = _context.Product
                .Where(p => p.Name.Contains(query) || p.Category.Contains(query))
                .ToList();

            return View(products);
        }
    }
}

