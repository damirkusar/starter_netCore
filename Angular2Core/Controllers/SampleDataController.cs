using Angular2Core.Dal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Angular2Core.Controllers
{
    [Authorize]
    [Route("api/sample")]
    public class SampleDataController : Controller
    {
        private readonly DataAccessLayer dal;

        public SampleDataController(DataAccessLayer dal)
        {
            this.dal = dal;
        }

        [HttpPost]
        [Route("CreateSampleData")]
        [AllowAnonymous]
        public void CreateSampleData()
        {
            this.dal.AddLocalization("en", "test", "test", "Just Testing");
        }

        [HttpGet]
        [Route("GetSampleData")]
        [AllowAnonymous]
        public IActionResult GetSampleData()
        {
            // Default reverse engineered models
            var localization = this.dal.GetLocalizations();
            var localizationAsJson = this.dal.GetLocalizationsAsJson();

            return this.Ok(localization);
        }

        [HttpGet]
        [Route("GetSampleDataSecured")]
        public IActionResult GetSampleDataSecured()
        {
            // Default reverse engineered models
            var localization = this.dal.GetLocalizations();
            var localizationAsJson = this.dal.GetLocalizationsAsJson();

            return this.Ok(localization);
        }
    }
}
