using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.DataAccessLayer;

namespace WebApp.Controllers
{
    [Authorize]
    [Route("api/sample")]
    public class SampleDataController : Controller
    {
        private readonly DataLayer dataLayer;

        public SampleDataController(DataLayer dataLayer)
        {
            this.dataLayer = dataLayer;
        }

        [HttpPost]
        [Route("CreateSampleData")]
        [AllowAnonymous]
        public void CreateSampleData()
        {
            this.dataLayer.AddLocalization("en", "test", "test", "Just Testing");
        }

        [HttpGet]
        [Route("GetSampleData")]
        [AllowAnonymous]
        public IActionResult GetSampleData()
        {
            try
            {
                var localization = this.dataLayer.GetLocalizations();
                return this.Ok(localization);
            }
            catch (Exception)
            {
                return this.BadRequest();
            }
        }

        [HttpGet]
        [Route("GetSampleDataSecured")]
        public IActionResult GetSampleDataSecured()
        {
            try
            {
                var localization = this.dataLayer.GetLocalizations();
                return this.Ok(localization);
            }
            catch (Exception)
            {
                return this.BadRequest();
            }
        }
    }
}