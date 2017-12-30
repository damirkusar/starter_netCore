using Microsoft.AspNetCore.Mvc;

namespace ResourceProvider.Controllers.Home
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return this.Redirect("/swagger");
        }
    }
}
