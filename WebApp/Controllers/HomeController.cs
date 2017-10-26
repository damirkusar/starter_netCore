using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return this.Redirect("/swagger");
        }

        public IActionResult Error()
        {
            //return this.InternalError();            
            return this.Redirect("/swagger");

        }
    }
}
