using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.DataAccessLayer;

namespace WebApp.Controllers
{
    [AllowAnonymous]
    [Route("api/localization")]
    public class LocalizationController : Controller
    {
        private readonly DataLayer dataLayer;

        public LocalizationController(DataLayer dataLayer)
        {
            this.dataLayer = dataLayer;
        }

        [HttpGet]
        [Route("{language}")]
        public IActionResult GetLocalization(string language)
        {
            return this.Ok(this.dataLayer.GetLocalizationsAsJson(language));
        }
    }
}