using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Web.Controllers
{
    public class ProductController : BaseController
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }
    }
}