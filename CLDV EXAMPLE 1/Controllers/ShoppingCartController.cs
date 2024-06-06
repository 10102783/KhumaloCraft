using KhumaloCraft.Data;
using KhumaloCraft.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KhumaloCraft.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly ProductContext _context;
        private readonly UserManager<AppUser> _userManager;

        public ShoppingCartController(ProductContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // Action to display the shopping cart
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            var shoppingCart = await _context.ShoppingCart
                .Include(sc => sc.ShoppingCartItems)
                    .ThenInclude(sci => sci.Product) // Include product information
                .FirstOrDefaultAsync(sc => sc.UserId == user.Id);

            if (shoppingCart == null)
            {
                shoppingCart = new ShoppingCart
                {
                    UserId = user.Id,
                    ShoppingCartItems = new List<ShoppingCartItem>()
                };
                _context.ShoppingCart.Add(shoppingCart);
                shoppingCart.TotalPrice = shoppingCart.ShoppingCartItems.Sum(item => item.Quantity * item.Product.Price);
                await _context.SaveChangesAsync();
            }

            decimal totalPrice = shoppingCart.ShoppingCartItems.Sum(item => item.Quantity * item.Product.Price);
            shoppingCart.TotalPrice = totalPrice;

            await _context.SaveChangesAsync();

            return View(shoppingCart);
        }

        // Action to add a product to the shopping cart
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddToCart([FromBody] AddToCartRequest request)
        {
            var user = await _userManager.GetUserAsync(User);
            var product = await _context.Product.FindAsync(request.ProductId);

            if (product == null)
            {
                return NotFound();
            }

            var shoppingCart = await _context.ShoppingCart
                .Include(sc => sc.ShoppingCartItems)
                .FirstOrDefaultAsync(sc => sc.UserId == user.Id);

            if (shoppingCart == null)
            {
                shoppingCart = new ShoppingCart
                {
                    UserId = user.Id,
                    ShoppingCartItems = new List<ShoppingCartItem>()
                };
                _context.ShoppingCart.Add(shoppingCart);
            }

            var cartItem = shoppingCart.ShoppingCartItems.FirstOrDefault(item => item.ProductId == request.ProductId);
            if (cartItem != null)
            {
                cartItem.Quantity += request.Quantity;
            }
            else
            {
                cartItem = new ShoppingCartItem
                {
                    ProductId = request.ProductId,
                    Quantity = request.Quantity
                };
                shoppingCart.ShoppingCartItems.Add(cartItem);
            }

            await _context.SaveChangesAsync();

            return Ok();
        }

        // Action to remove a product from the shopping cart
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> RemoveFromCart(int shoppingCartItemId)
        {
            var shoppingCartItem = await _context.ShoppingCartItem.FindAsync(shoppingCartItemId);

            if (shoppingCartItem != null)
            {
                _context.ShoppingCartItem.Remove(shoppingCartItem);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }

    public class AddToCartRequest
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
