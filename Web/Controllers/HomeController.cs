using Microsoft.AspNetCore.Mvc;
using Web.Repository;

namespace Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IRepository _repository;

        public HomeController(IRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Map()
        {
            return View();
        }

        public IActionResult Index()
        {
            var items = _repository.GetAllPosts();
            return View(items);
        }
    }
}