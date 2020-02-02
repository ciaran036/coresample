using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartBreadcrumbs.Attributes;

namespace Web.Pages
{
    [Authorize]
    [DefaultBreadcrumb("Dashboard")]
    public class IndexModel : PageModel
    {
        public void OnGet()
        {

        }
    }
}