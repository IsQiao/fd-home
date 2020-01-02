using Microsoft.AspNetCore.Mvc;
using Web.Managers;

namespace Web.Controllers
{
    public class PostController : Controller
    {
        private readonly IImageManager _imageManager;

        public PostController(IImageManager imageManager)
        {
            _imageManager = imageManager;
        }

        [HttpGet]
        public IActionResult Image(string image)
        {
            var mime = image.Substring(image.LastIndexOf(',') + 1);
            return new FileStreamResult(_imageManager.ImageStream(image), $"image/{mime}");
        }
    }
}