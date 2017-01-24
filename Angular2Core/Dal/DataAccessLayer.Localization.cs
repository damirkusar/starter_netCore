using System.Collections.Generic;
using Angular2Core.Models.DataDb;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace Angular2Core.Dal
{
    public partial class DataAccessLayer
    {
        public List<Localization> GetLocalizations()
        {
            return this.dataDbContext.Localizations.ToList();
        }

        public List<Localization> GetLocalizations(string language)
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

        public Localization AddLocalization(string language, string container, string key, string value)
        {
            var localization = new Localization()
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

        private string CreateLocalizationKey(Localization localization)
        {
            var key = localization.Container != null ? $"{localization.Container}_{localization.Key}" : $"{localization.Key}";
            return key;
        }
    }
}
