using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Dal;

namespace WebApp.Controllers
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
            try
            {
                var localization = this.dal.GetLocalizations();
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
                var localization = this.dal.GetLocalizations();
                return this.Ok(localization);
            }
            catch (Exception)
            {
                return this.BadRequest();
            }
        }
    }
}
