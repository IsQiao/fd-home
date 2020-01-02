using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Web.Managers
{
    public interface IImageManager
    {
        Task<string> SaveImageAsync(IFormFile image);

        FileStream ImageStream(string imageSrc);
    }
}