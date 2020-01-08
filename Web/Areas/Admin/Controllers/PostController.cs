using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Data;
using Web.Managers;
using Web.ViewModels;

namespace Web.Controllers.Admin
{
    [Area("Admin")]
    [Authorize]
    public class PostController : Controller
    {
        private readonly AppDbContext _dbContext;
        private IFileManager _fileManager;

        public PostController(AppDbContext dbContext, IFileManager fileManager)
        {
            _dbContext = dbContext;
            _fileManager = fileManager;
        }

        public IActionResult Index()
        {
            return View();
        }


        public async Task<IActionResult> Edit(int? id)
        {
            var options = await _dbContext.PostCategory.ToListAsync();
            ViewBag.AllOptions = options.Select(x => new
            {
                Value = x.Id,
                x.Name,
                x.PostType
            }).ToList();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(PostViewModel viewModel)
        {
            if (viewModel.Icon != null)
            {
                viewModel.IconSrc = await _fileManager.SaveImageAsync(viewModel.Icon);
            }

            if (viewModel.Id != 0)
            {
                _dbContext.Posts.Update(viewModel);
            }
            else
            {
                _dbContext.Posts.Add(viewModel);
            }

            await _dbContext.SaveChangesAsync();

            return View();
            // return RedirectToRoute("area", new
            // {
            //     area = "Admin",
            //     controller = "PostCategory",
            //     action = "List"
            // });
        }
    }
}