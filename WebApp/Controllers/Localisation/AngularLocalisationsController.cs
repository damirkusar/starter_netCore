using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using Swashbuckle.AspNetCore.SwaggerGen;
using WebApp.Filters;
using WebApp.Localisation.Interface;

namespace WebApp.Controllers.Localisation
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiExplorerSettings(IgnoreApi = false)]
    public class AngularLocalisationsController : Controller
    {
        private readonly ILogger<AngularLocalisationsController> logger;
        private readonly ILocalisationService localisationService;

        public AngularLocalisationsController(ILogger<AngularLocalisationsController> logger, ILocalisationService localisationService)
        {
            this.logger = logger;
            this.localisationService = localisationService;
        }

        [HttpGet]
        [ValidateModelState]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(JObject))]
        public async Task<IActionResult> GetLocalisations(string languageIsoAlpha2)
        {
            var localisations = await this.localisationService.GetJsonLocalisationsAsync(languageIsoAlpha2);
            return this.Ok(localisations);
        }
    }
}