using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Web.Data;
using Web.Models;

namespace Web.Managers
{
    public class FileManager : IFileManager
    {
        private readonly AppDbContext _dbContext;
        private readonly string _imagePath;
        private readonly string _savePath;

        public FileManager(AppDbContext dbContext,IConfiguration config)
        {
            _dbContext = dbContext;
            _imagePath = config["Path:Images"];
            _savePath = Path.Combine(_imagePath);
        }

        public async Task<string> SaveImageAsync(IFormFile image)
        {
            try
            {
                if (!Directory.Exists(_savePath))
                {
                    Directory.CreateDirectory(_savePath);
                }

                var mine = image.FileName.Substring(image.FileName.LastIndexOf(('.')));
                var fileName = $"img_{DateTime.Now:yyyMMddHHmmss}" + mine;
                await using (var fileStream = new FileStream(Path.Combine(_savePath, fileName), FileMode.Create))
                {
                    await image.CopyToAsync(fileStream);
                    await _dbContext.Uploads.AddAsync(new Upload()
                    {
                        FileName = fileName
                    });
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

        public void DeleteFileAsync(string fileName)
        {
            var fileSrc = Path.Combine(_savePath, fileName);
            if (!File.Exists(fileSrc)) return;
            File.Delete(fileSrc);

            var removedFile = _dbContext.Uploads.FirstOrDefault(x => x.FileName == fileName);
            if (removedFile != null)
                _dbContext.Uploads.Remove(removedFile);
        }
    }
}