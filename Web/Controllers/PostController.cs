using Microsoft.AspNetCore.Mvc;
using Web.Managers;

namespace Web.Controllers
{
    public class PostController : Controller
    {
        private readonly IFileManager _fileManager;

        public PostController(IFileManager fileManager)
        {
            _fileManager = fileManager;
        }

        [HttpGet]
        public IActionResult Image(string image)
        {
            var mime = image.Substring(image.LastIndexOf(',') + 1);
            return new FileStreamResult(_fileManager.ImageStream(image), $"image/{mime}");
        }
    }
}