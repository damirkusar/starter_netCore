using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog;
using WebApp.DataAccessLayer;
using WebApp.DataAccessLayer.Models;

namespace WebApp.Controllers
{
    [Authorize]
    [Route("api/sample")]
    public class SampleDataController : Controller
    {
        private readonly DataLayer dataLayer;
        private Logger logger;

        public SampleDataController(DataLayer dataLayer)
        {
            this.dataLayer = dataLayer;
            this.logger = LogManager.GetCurrentClassLogger();
        }

        [HttpPost]
        [Route("CreateSampleData")]
        [AllowAnonymous]
        public void CreateSampleData()
        {
            this.dataLayer.AddLocalisation(new Localisation {Key = "TestKey", Language = "DE", Value = "TestValue"});
        }

        [HttpGet]
        [Route("GetSampleData")]
        [AllowAnonymous]
        public IActionResult GetSampleData()
        {
            try
            {
                var localization = this.dataLayer.GetLocalisations();
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
                var localization = this.dataLayer.GetLocalisations();
                return this.Ok(localization);
            }
            catch (Exception)
            {
                return this.BadRequest();
            }
        }
    }
}
