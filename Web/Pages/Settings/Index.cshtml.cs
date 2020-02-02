using Data;
using Entities;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartBreadcrumbs.Attributes;

namespace Web.Pages.Settings
{
    [Breadcrumb("Settings")]
    public class IndexModel : PageModel
    {
        private readonly DataContext _dataContext;

        public SystemSettings SystemSettings { get; set; }

        public IndexModel(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void OnGet()
        {
            SystemSettings = _dataContext.Settings.Result;
        }
    }
}