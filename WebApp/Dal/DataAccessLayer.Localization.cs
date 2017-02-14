using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using WebApp.Models.DataDb;

namespace WebApp.Dal
{
    public partial class DataAccessLayer
    {
        public List<Localizations> GetLocalizations()
        {
            return this.dataDbContext.Localizations.ToList();
        }

        public List<Localizations> GetLocalizations(string language)
        {
            return this.dataDbContext.Localizations.Where(x => x.Language.Equals(language)).ToList();
        }

        public JObject GetLocalizationsAsJson()
        {
            var jObject = new JObject();
            var localizations = this.GetLocalizations();
            localizations.ForEach(x => jObject[this.CreateLocalizationKey(x)] = x.Value);
            return jObject;
        }

        public JObject GetLocalizationsAsJson(string language)
        {
            var jObject = new JObject();
            var localizations = this.GetLocalizations(language);
            localizations.ForEach(x => jObject[this.CreateLocalizationKey(x)] = x.Value);
            return jObject;
        }

        public Localizations AddLocalization(string language, string container, string key, string value)
        {
            var localization = new Localizations()
            {
                Language = language,
                Container = container,
                Key = key,
                Value = value
            };
            this.dataDbContext.Localizations.Add(localization);
            this.dataDbContext.SaveChanges();

            return localization;
        }

        private string CreateLocalizationKey(Localizations localization)
        {
            var key = localization.Container != null ? $"{localization.Container}_{localization.Key}" : $"{localization.Key}";
            return key;
        }
    }
}
