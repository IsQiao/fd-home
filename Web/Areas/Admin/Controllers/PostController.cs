using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web.Data;
using Web.Extensions;
using Web.Managers;
using Web.Models;
using Web.ViewModels;

namespace Web.Controllers.Admin
{
    [Area("Admin")]
    [Authorize]
    public class PostController : Controller
    {
        private readonly AppDbContext _dbContext;
        private readonly IFileManager _fileManager;
        private readonly IMapper _mapper;

        public PostController(AppDbContext dbContext, IFileManager fileManager, IMapper mapper)
        {
            _dbContext = dbContext;
            _fileManager = fileManager;
            _mapper = mapper;
        }

        public async Task<IActionResult> Edit(int? id)
        {
            var options = await _dbContext.PostCategory.ToListAsync();

            ViewBag.AllOptions = options
                .Where(x => x.PostType == PostType.Product)
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Name
                }).ToList();

            if (id != null)
            {
                var item = await _dbContext.Posts.FirstOrDefaultAsync(x => x.Id == id);
                return View(_mapper.Map<PostViewModel>(item));
            }

            return View();
        }

        public async Task<IActionResult> Delete(int id)
        {
            var item = await _dbContext.Posts.FirstOrDefaultAsync(x => x.Id == id);
            
            if (item != null)
            {
                _dbContext.Remove(item);
                await _dbContext.SaveChangesAsync();
            }

            return RedirectToRoute("area", new
            {
                area = "Admin",
                controller = "Post",
                action = "List"
            });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(PostViewModel viewModel)
        {
            if (viewModel.Icon != null)
            {
                viewModel.IconSrc = await _fileManager.SaveImageAsync(viewModel.Icon);
            }

            if (viewModel.PostCategoryId != null)
            {
                viewModel.PostCategory =
                    await _dbContext.PostCategory.FirstOrDefaultAsync(x => x.Id == viewModel.PostCategoryId);
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

            return RedirectToRoute("area", new
            {
                area = "Admin",
                controller = "Post",
                action = "List"
            });
        }

        public IActionResult List(PostPagerRequest viewModel)
        {
            ViewBag.PagerFilter = viewModel;

            var list = _dbContext
                .Posts
                .Where(x => viewModel.PostType == null || x.PostType == viewModel.PostType)
                .Where(x => viewModel.SearchString == null || x.Title.Contains(viewModel.SearchString))
                .Include(x => x.PostCategory)
                .OrderByDescending(x => x.Created);

            return View(_mapper.Map<IEnumerable<PostListViewModel>>(list)
                .GetPaged(viewModel.PageNo, viewModel.PageSize));
        }
    }
}