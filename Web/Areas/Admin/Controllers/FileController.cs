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
    public class FileController : Controller
    {
        private readonly IFileManager _fileManager;
        private AppDbContext _dbContext;

        public FileController(AppDbContext dbContext, IFileManager fileManager)
        {
            _dbContext = dbContext;
            _fileManager = fileManager;
        }

        [HttpPost]
        public async Task<IActionResult> Image(EditorUploadRequest vm)
        {
            if (vm.File == null)
                return Json(new EditorUploadViewModel
                {
                    Success = false
                });

            var fileName = await _fileManager.SaveImageAsync(vm.File);
            return Json(new EditorUploadViewModel
            {
                Success = true,
                File = $"/content/blog/{fileName}"
            });
        }
    }
}