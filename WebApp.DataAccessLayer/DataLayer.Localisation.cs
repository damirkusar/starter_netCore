using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using WebApp.DataAccessLayer.Views;

namespace WebApp.DataAccessLayer
{
    public partial class DataLayer
    {
        public virtual List<Localisation> GetLocalisations()
        {
            return this.dataDbContext.Localisations.ToList();
        }

        public virtual List<Localisation> GetLocalisations(string language)
        {
            return this.dataDbContext.Localisations.Where(x => x.LanguageIsoAlpha2.Equals(language)).ToList();
        }

        public virtual JObject GetLocalisationsAsJson()
        {
            var jObject = new JObject();
            var localizations = this.GetLocalisations();
            localizations.ForEach(x => jObject[this.CreateLocalizationKey(x)] = x.Value);
            return jObject;
        }

        public virtual JObject GetLocalisationsAsJson(string language)
        {
            var jObject = new JObject();
            var localizations = this.GetLocalisations(language);
            localizations.ForEach(x => jObject[this.CreateLocalizationKey(x)] = x.Value);
            return jObject;
        }

        public virtual Localisation UpdateLocalisation(Localisation localisation)
        {
            this.dataDbContext.Update(localisation);
            this.dataDbContext.SaveChanges();

            return localisation;
        }

        public virtual Localisation AddLocalisation(Localisation localisation)
        {
            this.dataDbContext.Localisations.Add(localisation);
            this.dataDbContext.SaveChanges();

            return localisation;
        }

        public virtual Localisation RemoveLocalisation(Localisation localisation)
        {
            this.dataDbContext.Localisations.Remove(localisation);
            this.dataDbContext.SaveChanges();

            return localisation;
        }

        public virtual string CreateLocalizationKey(Localisation localization)
        {
            //var key = localization.Container != null ? $"{localization.}_{localization.Key}" : $"{localization.Key}";
            var key = $"{localization.Key}";
            return key;
        }
    }
}
