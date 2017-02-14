using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Dal;

namespace WebApp.Controllers
{
    [AllowAnonymous]
    [Route("api/localization")]
    public class LocalizationController : Controller
    {
        private readonly DataAccessLayer dal;

        public LocalizationController(DataAccessLayer dal)
        {
            this.dal = dal;
        }

        [HttpGet]
        [Route("{language}")]
        public IActionResult GetLocalization(string language)
        {
            return this.Ok(this.dal.GetLocalizationsAsJson(language));
        }
    }
}