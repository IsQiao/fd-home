using Microsoft.AspNetCore.Http;
using Web.Models;

namespace Web.ViewModels
{
    public class PostViewModel : Post
    {
        public IFormFile Icon { get; set; }

        public int? PostCategoryId { get; set; }
    }
}