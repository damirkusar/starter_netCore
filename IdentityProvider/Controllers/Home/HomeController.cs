using Microsoft.AspNetCore.Mvc;

namespace IdentityProvider.Controllers.Home
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return this.Redirect("/swagger");
        }
    }
}
