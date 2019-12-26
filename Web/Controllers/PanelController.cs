using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PanelController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}