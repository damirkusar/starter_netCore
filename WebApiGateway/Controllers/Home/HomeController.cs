using Microsoft.AspNetCore.Mvc;

namespace WebApiGateway.Controllers.Home
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return this.Redirect("/swagger");
        }
    }
}
