using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Web.ViewComponents
{
    public class Navigation : ViewComponent
    {
        // TODO: Initialise dependencies
        public Navigation()
        {
            
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            // TODO: Add logic
            return View();
        }
    }
}