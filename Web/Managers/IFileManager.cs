using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Web.Managers
{
    public interface IFileManager
    {
        Task<string> SaveImageAsync(IFormFile image);

        FileStream ImageStream(string imageSrc);

        void DeleteFileAsync(string fileName);
    }
}