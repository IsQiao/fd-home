using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
            return View();
        }
    }
}