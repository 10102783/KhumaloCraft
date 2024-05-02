using Microsoft.AspNetCore.Mvc;
using KhumaloCraft.Models;
using Microsoft.AspNetCore.Identity;



namespace KhumaloCraft.Controllers
{
    public class AccountController : Controller
    {

        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // GET: /Account/Register
        [HttpGet]
        public IActionResult Register()
        {
            var model = new RegisterModel.InputModel(); 
            return View(model);
        }

        // POST: /Account/Register
        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel.InputModel model, bool isAdmin = false)
        {
            if (ModelState.IsValid)
            {
                var user = new AppUser { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    if (isAdmin)
                    {
                        await _userManager.AddToRoleAsync(user, "Admin");
                    }
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
                return View(model);
            }


        // GET: /Account/Login
        [HttpGet]
        public IActionResult Login()
        {
            var model = new LoginModel.InputModel();
            return View(model);
        }

        // POST: /Account/Login
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel.InputModel model, string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    var user = await _userManager.FindByEmailAsync(model.Email);
                    if (await _userManager.IsInRoleAsync(user, "Admin"))
                    {
                        return RedirectToAction("AdminPage", "Admin");
                    }

                    return LocalRedirect(returnUrl);

                }
                ModelState.AddModelError(string.Empty, "Invalid Login attempt.");
            }

            return View(model);
        }

        // Authenticate user and redirect to appropriate page

    [HttpPost]
     public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();    
            return RedirectToAction("Index", "Home");

        }
            
        }

    }



