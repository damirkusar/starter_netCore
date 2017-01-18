using Angular2Core.Models.DataDb;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Angular2Core.Controllers
{
    [AllowAnonymous]
    [Route("api/sample")]
    public class SampleDataController : Controller
    {
        private readonly DataDbContext dataDbContext;

        public SampleDataController(DataDbContext dataDbContext)
        {
            this.dataDbContext = dataDbContext;
        }

        [HttpPost]
        [Route("CreateSampleData")]
        public void CreateSampleData()
        {
            this.dataDbContext.Localizations.Add(new Localization() { Language = "de", Container = "home", Key = "title", Value = "Willkommen c" });
            this.dataDbContext.Localizations.Add(new Localization() { Language = "en", Container = "home", Key = "title", Value = "Welcome c" });
            this.dataDbContext.Localizations.Add(new Localization() { Language = "de", Key = "welcome", Value = "Willkommen" });
            this.dataDbContext.Localizations.Add(new Localization() { Language = "en", Key = "welcome", Value = "Welcome" });
            this.dataDbContext.SaveChanges();
        }
    }
}
