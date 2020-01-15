using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Web.Data;
using Web.Extensions;
using Web.Models;
using Web.Repository;
using Web.ViewModels;

namespace Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly AppDbContext _appDbContext;

        public HomeController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IActionResult Map()
        {
            return View();
        }

        public IActionResult Index()
        {
            var vm = new HomeViewModel
            {
                Products = _appDbContext.Posts
                    .Where(x => x.PostType == PostType.Product)
                    .OrderByDescending(x => x.Created)
                    .ThenBy(x => x.IconSrc).Take(7)
                    .ToList(),
                AboutUs = _appDbContext.Posts
                    .FirstOrDefault(x => x.PostType == PostType.AboutUs),
                ProductCategoryList = _appDbContext.PostCategory
                    .Where(x => x.PostType == PostType.Product)
                    .Take(3)
                    .ToList()
            };

            if (vm.AboutUs != null)
            {
                vm.AboutUs.Body = vm.AboutUs.Body.StripHTMLTags();
            }

            return View(vm);
        }
    }
}