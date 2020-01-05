using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        private IImageManager _imageManager;

        public PostController(AppDbContext dbContext, IImageManager imageManager)
        {
            _dbContext = dbContext;
            _imageManager = imageManager;
        }

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Edit(int? id)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(PostViewModel viewModel)
        {
            if (viewModel.Icon != null)
            {
                viewModel.IconSrc = await _imageManager.SaveImageAsync(viewModel.Icon);
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