using System.Linq;
using Angular2Core.Models.DataDb;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace Angular2Core.Controllers
{
    [AllowAnonymous]
    [Route("api/localization")]
    public class LocalizationController : Controller
    {
        private readonly DataDbContext dataDbContext;

        public LocalizationController(DataDbContext dataDbContext)
        {
            this.dataDbContext = dataDbContext;
        }

        [HttpGet]
        [Route("{language}")]
        public IActionResult GetLocalization(string language)
        {
            var jObject = new JObject();
            var localizations = this.dataDbContext.Localizations.Where(x => x.Language.Equals(language)).ToList();
            localizations.ForEach(x => jObject[this.CreateKey(x)] = x.Value);
            return this.Ok(jObject);
        }

        private string CreateKey(Localization localization)
        {
            var key = localization.Container != null ? $"{localization.Container}_{localization.Key}" : $"{localization.Key}";
            return key;
        }
    }
}