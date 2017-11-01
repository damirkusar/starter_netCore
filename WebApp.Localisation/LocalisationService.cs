using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using WebApp.Localisation.DataAccessLayer;
using WebApp.Localisation.Interface;

namespace WebApp.Localisation
{
    public class LocalisationService: ILocalisationService
    {
        private readonly ILogger<LocalisationService> logger;
        private readonly ILocalisationDbContext dbContext;

        public LocalisationService(ILogger<LocalisationService> logger, ILocalisationDbContext dbContext)
        {
            this.logger = logger;
            this.dbContext = dbContext;
        }

        public async Task<JObject> GetJsonLocalisationsAsync(string languageIsoAlpha2)
        {
            var jObject = new JObject();
            var localisations = await this.dbContext.Localisations.Where(x => x.LanguageIsoAlpha2.Equals(languageIsoAlpha2)).ToListAsync();

            localisations.ForEach(x => jObject[x.Key] = x.Value);
            return jObject;
        }
    }
}