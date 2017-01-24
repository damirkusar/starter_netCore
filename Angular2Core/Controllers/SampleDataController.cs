using Angular2Core.Dal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Angular2Core.Controllers
{
    [AllowAnonymous]
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
        public void CreateSampleData()
        {
            this.dal.AddLocalization("en", "test", "test", "Just Testing");
        }

        [HttpGet]
        [Route("GetSampleData")]
        public IActionResult GetSampleData()
        {
            // Default reverse engineered models
            var localization = this.dal.GetLocalizations();
            var localizationAsJson = this.dal.GetLocalizationsAsJson();

            return this.Ok(localization);
        }
    }
}
