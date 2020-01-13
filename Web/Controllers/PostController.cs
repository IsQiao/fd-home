using System.Collections.Generic;
using System.Linq;
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

        public IActionResult ProductList(PostPagerRequest viewModel)
        {
            viewModel.PageSize = 5;
            ViewBag.PagerFilter = viewModel;

            var list = _dbContext
                .Posts
                .Where(x => x.PostType == PostType.Product)
                .Include(x => x.PostCategory)
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