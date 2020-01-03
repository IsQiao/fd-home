using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers.Admin
{
    [Area("Admin")]
    public class PostController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}