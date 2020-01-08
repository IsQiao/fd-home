using System;

namespace Web.Models
{
    public class Upload
    {
        public int Id { get; set; }

        public string FileName { get; set; }
        
        public DateTime Created { get; set; } = DateTime.Now;
    }
}