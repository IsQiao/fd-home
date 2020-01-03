using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Web.ViewModels;

namespace Web.Controllers.Admin
{
    [Area("Admin")]
    [Authorize]
    public class AuthController : Controller
    {
        private readonly SignInManager<IdentityUser> _sinInManager;

        public AuthController(SignInManager<IdentityUser> signInManager)
        {
            _sinInManager = signInManager;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel vm)
        {
            var result = await _sinInManager.PasswordSignInAsync(vm.UserName, vm.Password, false, false);

            if (Url.IsLocalUrl(vm.ReturnUrl))
                return Redirect(vm.ReturnUrl);

            return RedirectToRoute("area", new
            {
                area = "Admin",
                controller = "Home",
                action = "Index"
            });
        }

        [AllowAnonymous]
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

        [HttpPost]
        public async Task<IActionResult> UserInfo(ChangePasswordViewModel viewModel)
        {
            if (viewModel.NewPassword != viewModel.NewPasswordConfirmed ||
                string.IsNullOrWhiteSpace(viewModel.NewPassword))
            {
                return View("ChangePasswordFailed");
            }

            var currentUser = await _sinInManager.UserManager.GetUserAsync(HttpContext.User);
            if (currentUser != null)
            {
                await _sinInManager.UserManager.RemovePasswordAsync(currentUser);
                await _sinInManager.UserManager.AddPasswordAsync(currentUser, viewModel.NewPassword);
            }
            else
            {
                return View("ChangePasswordFailed");
            }

            await _sinInManager.SignOutAsync();
            return View("ChangePasswordSucceed");
        }
    }
}