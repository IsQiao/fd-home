using Microsoft.AspNetCore.Http;

namespace Web.ViewModels
{
    public class EditorUploadViewModel
    {
        public bool Success { get; set; }

        public string File { get; set; }
    }

    public class EditorUploadRequest
    {
        public string Alt { get; set; }

        public IFormFile File { get; set; }
    }
}