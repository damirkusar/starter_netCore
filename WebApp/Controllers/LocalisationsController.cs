using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.DataAccessLayer;
using WebApp.DataAccessLayer.Models;

namespace WebApp.Controllers
{
    [AllowAnonymous]
    [Route("api/localisations")]
    public class LocalisationsController : Controller
    {
        private readonly DataLayer dataLayer;

        public LocalisationsController(DataLayer dataLayer)
        {
            this.dataLayer = dataLayer;
        }

        [HttpGet]
        [Route("")]
        public IActionResult GetLocalisations()
        {
            try
            {
                return this.Ok(this.dataLayer.GetLocalisationsAsJson());
            }
            catch (Exception)
            {
                return this.BadRequest();
            }
        }

        [HttpGet]
        [Route("{language}")]
        public IActionResult GetLocalisations(string language)
        {
            try
            {
                return this.Ok(this.dataLayer.GetLocalisationsAsJson(language));
            }
            catch (Exception)
            {
                return this.BadRequest();
            }
        }

        [HttpPut]
        [Route("{key}")]
        public IActionResult UpdateLocalisation(string key, Localisation localisation)
        {
            try
            {
                return this.Ok(this.dataLayer.UpdateLocalisation(localisation));
            }
            catch (Exception)
            {
                return this.BadRequest();
            }
        }

        [HttpPost]
        [Route("")]
        public IActionResult CreateLocalisation(Localisation localisation)
        {
            try
            {
                return this.Ok(this.dataLayer.AddLocalisation(localisation));
            }
            catch (Exception)
            {
                return this.BadRequest();
            }
        }
    }
}