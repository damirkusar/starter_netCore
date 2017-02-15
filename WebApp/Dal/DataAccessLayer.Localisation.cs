using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using WebApp.Models.DataDb;

namespace WebApp.Dal
{
    public partial class DataAccessLayer
    {
        public virtual List<Localisation> GetLocalizations()
        {
            return this.dataDbContext.Localizations.ToList();
        }

        public virtual List<Localisation> GetLocalizations(string language)
        {
            return this.dataDbContext.Localizations.Where(x => x.Language.Equals(language)).ToList();
        }

        public virtual JObject GetLocalizationsAsJson()
        {
            var jObject = new JObject();
            var localizations = this.GetLocalizations();
            localizations.ForEach(x => jObject[this.CreateLocalizationKey(x)] = x.Value);
            return jObject;
        }

        public virtual JObject GetLocalizationsAsJson(string language)
        {
            var jObject = new JObject();
            var localizations = this.GetLocalizations(language);
            localizations.ForEach(x => jObject[this.CreateLocalizationKey(x)] = x.Value);
            return jObject;
        }

        public virtual Localisation AddLocalization(string language, string container, string key, string value)
        {
            var localization = new Localisation()
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

        private string CreateLocalizationKey(Localisation localization)
        {
            var key = localization.Container != null ? $"{localization.Container}_{localization.Key}" : $"{localization.Key}";
            return key;
        }
    }
}
