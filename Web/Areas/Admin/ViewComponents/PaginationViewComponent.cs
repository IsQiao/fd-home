using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Web.ViewModels;

namespace Web.Areas.Admin.ViewComponents
{
    public class PaginationViewComponent : ViewComponent
    {
        public Task<IViewComponentResult> InvokeAsync(PagedResultBase result)
        {
            return Task.FromResult((IViewComponentResult) View("Default", result));
        }
    }
}