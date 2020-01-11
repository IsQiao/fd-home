using System;

namespace Web.ViewModels
{
    public class PostListViewModel
    {
        public int Id { get; set; }

        public string PostTypeDisplay { get; set; }

        public string PostCategoryDisplay { get; set; }

        public DateTime CreatedTime { get; set; }

        public string Name { get; set; }
    }
}