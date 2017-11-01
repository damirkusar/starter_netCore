using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApp.Localisation.Interface;

namespace WebApp.Controllers.localisation
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiExplorerSettings(IgnoreApi = false)]
    public class UiLocalisationsController : Controller
    {
        private ILogger<UiLocalisationsController> logger;
        private readonly ILocalisationService localisationService;

        public UiLocalisationsController(ILogger<UiLocalisationsController> logger, ILocalisationService localisationService)
        {
            this.logger = logger;
            this.localisationService = localisationService;
        }

        [HttpGet]
        [Route("{languageIsoAlpha2}")]
        public async Task<IActionResult> GetLocalisations(string languageIsoAlpha2)
        {
            var localisations = await this.localisationService.GetJsonLocalisationsAsync(languageIsoAlpha2);
            return this.Ok(localisations);
        }
    }
}