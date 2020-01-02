using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Web.Data;
using Web.Models;
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

        // GET
        public IActionResult Index()
        {
            var items = _repository.GetAllPosts();
            return View(items);
        }

        public IActionResult Post(int id)
        {
            var item = _repository.GetPost(id);
            if (item != null)
                return View(item);
            return NotFound();
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
                return View(new Post());

            var item = _repository.GetPost(id.Value);
            return View(item);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Post post)
        {
            if (post.Id > 0)
                _repository.UpdatePost(post);
            else
                _repository.AddPost(post);

            if (await _repository.SaveChangesAsync())
            {
                return RedirectToAction("Index");
            }

            return View(post);
        }

        [HttpGet]
        public async Task<IActionResult> Remove(int id)
        {
            _repository.RemovePost(id);
            await _repository.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}