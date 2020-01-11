using System;
using System.Collections.Generic;
using System.Linq;
using Web.Models;
using Web.ViewModels;

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
                PostType.AboutUs => "关于我们",
                PostType.ContactUs => "联系我们",
                PostType.Display => "空间展示",
                PostType.Support => "技术支持",
                _ => ""
            };
        }

        public static PagedResult<T> GetPaged<T>(this IEnumerable<T> query,
            int page, int pageSize) where T : class
        {
            var result = new PagedResult<T>
            {
                CurrentPage = page,
                PageSize = pageSize,
                RowCount = query.Count()
            };

            var pageCount = (double) result.RowCount / pageSize;
            result.PageCount = (int) Math.Ceiling(pageCount);

            var skip = (page - 1) * pageSize;
            result.Results = query.Skip(skip).Take(pageSize).ToList();

            return result;
        }
    }
}