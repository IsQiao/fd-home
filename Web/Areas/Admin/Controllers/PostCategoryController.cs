using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Data;
using Web.Models;

namespace Web.Controllers.Admin
{
    [Area("Admin")]
    [Authorize]
    public class PostCategoryController : Controller
    {
        private readonly AppDbContext _dbContext;

        public PostCategoryController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(PostCategory vm)
        {
            if (ModelState.IsValid)
            {
                var exist = _dbContext.PostCategory.FirstOrDefault(x => x.Name == vm.Name && x.PostType == vm.PostType);
                if (exist == null)
                {
                    await _dbContext.PostCategory.AddAsync(vm);
                    await _dbContext.SaveChangesAsync();
                }
            }

            return RedirectToRoute("area", new
            {
                area = "Admin",
                controller = "PostCategory",
                action = "List"
            });
        }

        public async Task<IActionResult> List()
        {
            var list = await _dbContext.PostCategory.ToListAsync();
            return View(list);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                var item = await _dbContext.PostCategory.FirstOrDefaultAsync(x => x.Id == id);
                if (item != null)
                {
                    _dbContext.PostCategory.Remove(item);
                    await _dbContext.SaveChangesAsync();
                }
            }

            return RedirectToRoute("area", new
            {
                area = "Admin",
                controller = "PostCategory",
                action = "List"
            });
        }
    }
}