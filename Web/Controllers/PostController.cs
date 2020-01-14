using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Data;
using Web.Extensions;
using Web.Models;
using Web.ViewModels;

namespace Web.Controllers
{
    public class PostController : BaseController
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public PostController(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public IActionResult NewsList(PostPagerRequest viewModel)
        {
            viewModel.PageSize = 5;
            ViewBag.PagerFilter = viewModel;

            var list = _dbContext
                .Posts
                .Where(x => x.PostType == PostType.News)
                .Include(x => x.PostCategory)
                .OrderByDescending(x => x.Created);

            return View(_mapper.Map<IEnumerable<PostListViewModel>>(list)
                .GetPaged(viewModel.PageNo, viewModel.PageSize));
        }

        public IActionResult NewsContent(int id)
        {
            var item = _dbContext.Posts
                .Where(x => x.PostType == PostType.News)
                .FirstOrDefault(x => x.Id == id);
            return View(item);
        }

        public IActionResult Display()
        {
            var item = _dbContext.Posts.FirstOrDefault(x => x.PostType == PostType.Display);
            return View(item);
        }


        public IActionResult ContactUs()
        {
            var item = _dbContext.Posts.FirstOrDefault(x => x.PostType == PostType.ContactUs);
            return View(item);
        }


        public IActionResult AboutUs()
        {
            var item = _dbContext.Posts.FirstOrDefault(x => x.PostType == PostType.AboutUs);
            return View(item);
        }

        public IActionResult Support()
        {
            var item = _dbContext.Posts.FirstOrDefault(x => x.PostType == PostType.Support);
            return View(item);
        }

        public IActionResult ProductCategory()
        {
            var list = _dbContext
                .PostCategory
                .Where(x => x.PostType == PostType.Product);

            return View(list);
        }

        public async Task<IActionResult> ProductList(PostPagerRequest viewModel, int? categoryId)
        {
            viewModel.PageSize = 5;
            ViewBag.PagerFilter = viewModel;

            ViewBag.CategoryName = null;
            if (categoryId != null)
            {
                var category = await _dbContext
                    .PostCategory
                    .FirstOrDefaultAsync(x => x.Id == categoryId);
                ViewBag.CategoryName = category.Name;
            }

            var list = _dbContext
                .Posts
                .Include(x => x.PostCategory)
                .Where(x => categoryId == null || x.PostCategory.Id == categoryId)
                .Where(x => x.PostType == PostType.Product)
                .OrderByDescending(x => x.Created);

            return View(_mapper.Map<IEnumerable<PostListViewModel>>(list)
                .GetPaged(viewModel.PageNo, viewModel.PageSize));
        }

        public IActionResult ProductContent(int id)
        {
            var item = _dbContext.Posts
                .Where(x => x.PostType == PostType.Product)
                .FirstOrDefault(x => x.Id == id);
            return View(item);
        }
    }
}