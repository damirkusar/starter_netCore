using Microsoft.AspNetCore.Mvc;

namespace ApiGateway.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return this.Redirect("/swagger");
        }
    }
}
