using System.Collections;
using System.Collections.Generic;
using Web.Models;

namespace Web.ViewModels
{
    public class HomeViewModel
    {
        public IList<PostCategory> ProductCategoryList { get; set; }

        public IList<Post> Products { get; set; }

        public Post AboutUs { get; set; }
    }
}