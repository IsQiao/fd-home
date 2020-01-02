using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Web.Controllers
{
    public abstract class BaseController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ViewBag.Title = "凡渡";
            base.OnActionExecuting(filterContext);
        }
    }
}