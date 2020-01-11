using Web.Models;

namespace Web.ViewModels
{
    public class PostPagerRequest : PagerViewModel
    {
        public PostType? PostType { get; set; }
    }
}