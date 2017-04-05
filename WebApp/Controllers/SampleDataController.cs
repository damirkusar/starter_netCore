using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog;
using WebApp.DataAccessLayer;
using WebApp.DataAccessLayer.Models;

namespace WebApp.Controllers
{
    [AllowAnonymous]
    [Route("api/sample")]
    [ApiExplorerSettings(IgnoreApi = false)]
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
        public void CreateSampleData()
        {
            this.dataLayer.AddLocalisation(new Localisation {Key = "TestKey", Language = "DE", Value = "TestValue"});
        }

        [HttpGet]
        [Route("GetSampleData")]
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
        [Authorize]
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

        [HttpGet]
        [Route("GetSampleDataSecuredAdmin")]
        [Authorize(Roles = "admin")]
        public IActionResult GetSampleDataSecuredAdmin()
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
        [Route("GetSampleDataSecuredSuperAdmin")]
        [Authorize(Roles = "superadmin")]
        public IActionResult GetSampleDataSecuredSuperAdmin()
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
