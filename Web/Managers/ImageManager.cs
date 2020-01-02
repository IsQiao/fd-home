using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Web.Managers
{
    public class ImageManager : IImageManager
    {
        private readonly string _imagePath;

        public ImageManager(IConfiguration config)
        {
            _imagePath = config["Path:Images"];
        }

        public async Task<string> SaveImageAsync(IFormFile image)
        {
            try
            {
                var savePath = Path.Combine(_imagePath);
                if (!Directory.Exists(savePath))
                {
                    Directory.CreateDirectory(savePath);
                }

                var mine = image.FileName.Substring(image.FileName.LastIndexOf(('.')));
                var fileName = $"img_{DateTime.Now:yyyMMddHHmmss}";
                await using (var fileStream = new FileStream(Path.Combine(savePath, fileName), FileMode.Create))
                {
                    image.CopyToAsync(fileStream);
                }

                return fileName;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return "Error";
            }
        }

        public FileStream ImageStream(string imageSrc)
        {
            return new FileStream(Path.Combine(_imagePath, imageSrc), FileMode.Open, FileAccess.Read);
        }
    }
}