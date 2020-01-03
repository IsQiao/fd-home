using Web.Models;

namespace Web.Extensions
{
    public static class WebExtensions
    {
        public static string ToPostTypeDisplayName(this PostType postType)
        {
            return postType switch
            {
                PostType.News => "新闻",
                PostType.Product => "产品",
                _ => ""
            };
        }
    }
}