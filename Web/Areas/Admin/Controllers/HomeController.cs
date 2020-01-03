using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers.Admin
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}