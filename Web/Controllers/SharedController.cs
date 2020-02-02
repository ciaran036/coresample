using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class SharedController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return
            View();
        }
    }
}