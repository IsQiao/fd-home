using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Web.ViewModels;

namespace Web.Controllers.Admin
{
    [Area("Admin")]
    public class AuthController : Controller
    {
        private readonly SignInManager<IdentityUser> _sinInManager;

        public AuthController(SignInManager<IdentityUser> signInManager)
        {
            _sinInManager = signInManager;
        }

        // GET
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel vm)
        {
            var result = await _sinInManager.PasswordSignInAsync(vm.UserName, vm.Password, false, false);

            return RedirectToRoute("area", new
            {
                area = "Admin",
                controller = "Home",
                action = "Index"
            });
        }

        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _sinInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult UserInfo()
        {
            return View();
        }
    }
}