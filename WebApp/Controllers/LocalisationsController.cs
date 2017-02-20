using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog;
using NuGet.Protocol.Core.v3;
using WebApp.DataAccessLayer;
using WebApp.DataAccessLayer.Models;

namespace WebApp.Controllers
{
    [AllowAnonymous]
    [Route("api/localisations")]
    public class LocalisationsController : Controller
    {
        private readonly DataLayer dataLayer;
        private readonly Logger logger;

        public LocalisationsController(DataLayer dataLayer)
        {
            this.dataLayer = dataLayer;
            this.logger = LogManager.GetCurrentClassLogger();
        }

        [HttpGet]
        [Route("")]
        public IActionResult GetLocalisations()
        {
            try
            {
                this.logger.Trace($"GetLocalisations called");
                return this.Ok(this.dataLayer.GetLocalisations());
            }
            catch (Exception exception)
            {
                this.logger.Error(exception, $"Error in GetLocalisations");
                return this.BadRequest(exception);
            }
        }

        [HttpGet]
        [Route("{language}")]
        public IActionResult GetLocalisations(string language)
        {
            try
            {
                this.logger.Trace($"GetLocalisations called with language param: {language}");
                return this.Ok(this.dataLayer.GetLocalisations(language));
            }
            catch (Exception exception)
            {
                this.logger.Error(exception, $"Error in GetLocalisations with language param: {language}");
                return this.BadRequest(exception);
            }
        }

        [HttpGet]
        [Route("json")]
        public IActionResult GetLocalisationsAsJson()
        {
            try
            {
                this.logger.Trace($"GetLocalisationsAsJson called");
                return this.Ok(this.dataLayer.GetLocalisationsAsJson());
            }
            catch (Exception exception)
            {
                this.logger.Error(exception, $"Error in GetLocalisationsAsJson");
                return this.BadRequest(exception);
            }
        }

        [HttpGet]
        [Route("json/{language}")]
        public IActionResult GetLocalisationsAsJson(string language)
        {
            try
            {
                this.logger.Trace($"GetLocalisationsAsJson called with language param: {language}");
                return this.Ok(this.dataLayer.GetLocalisationsAsJson(language));
            }
            catch (Exception exception)
            {
                this.logger.Error(exception, $"Error in GetLocalisations with language param: {language}");
                return this.BadRequest(exception);
            }
        }

        [HttpPut]
        [Route("")]
        public IActionResult UpdateLocalisation([FromBody] Localisation localisation)
        {
            try
            {
                this.logger.Trace($"UpdateLocalisation called");
                return this.Ok(this.dataLayer.UpdateLocalisation(localisation));
            }
            catch (Exception exception)
            {
                this.logger.Error(exception, $"Error in UpdateLocalisation with localisation object: {localisation.ToJson()}");
                return this.BadRequest(exception);
            }
        }

        [HttpPost]
        [Route("")]
        public IActionResult AddLocalisation([FromBody] Localisation localisation)
        {
            try
            {
                this.logger.Trace($"AddLocalisation called");
                return this.Ok(this.dataLayer.AddLocalisation(localisation));
            }
            catch (Exception exception)
            {
                this.logger.Error($"Error in AddLocalisation with localisation object: {localisation.ToJson()}");
                return this.BadRequest(exception);
            }
        }

        [HttpDelete]
        [Route("")]
        public IActionResult RemoveLocalisation([FromBody] Localisation localisation)
        {
            try
            {
                this.logger.Trace($"RemoveLocalisation called");
                return this.Ok(this.dataLayer.RemoveLocalisation(localisation));
            }
            catch (Exception exception)
            {
                this.logger.Error($"Error in RemoveLocalisation with localisation object: {localisation.ToJson()}");
                return this.BadRequest(exception);
            }
        }
    }
}