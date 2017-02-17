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
                return this.Ok(this.dataLayer.GetLocalisations());
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
                return this.Ok(this.dataLayer.GetLocalisations(language));
            }
            catch (Exception)
            {
                return this.BadRequest();
            }
        }

        [HttpGet]
        [Route("json")]
        public IActionResult GetLocalisationsAsJson()
        {
            try
            {
                return this.Ok(this.dataLayer.GetLocalisationsAsJson());
            }
            catch (Exception exception)
            {
                return this.BadRequest(exception);
            }
        }

        [HttpGet]
        [Route("json/{language}")]
        public IActionResult GetLocalisationsAsJson(string language)
        {
            try
            {
                return this.Ok(this.dataLayer.GetLocalisationsAsJson(language));
            }
            catch (Exception exception)
            {
                return this.BadRequest(exception);
            }
        }

        [HttpPut]
        [Route("")]
        public IActionResult UpdateLocalisation(Localisation localisation)
        {
            try
            {
                return this.Ok(this.dataLayer.UpdateLocalisation(localisation));
            }
            catch (Exception exception)
            {
                return this.BadRequest(exception);
            }
        }

        [HttpPost]
        [Route("")]
        public IActionResult AddLocalisation(Localisation localisation)
        {
            try
            {
                return this.Ok(this.dataLayer.AddLocalisation(localisation));
            }
            catch (Exception exception)
            {
                return this.BadRequest(exception);
            }
        }

        [HttpDelete]
        [Route("")]
        public IActionResult RemoveLocalisation(Localisation localisation)
        {
            try
            {
                return this.Ok(this.dataLayer.RemoveLocalisation(localisation));
            }
            catch (Exception exception)
            {
                return this.BadRequest(exception);
            }
        }
    }
}