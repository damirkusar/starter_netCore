using System;

namespace WebApp.Localisation.DataAccessLayer.Models
{
    public class Localisation
    {
        public Guid LocalisationId { get; set; }
        public string LanguageIsoAlpha2 { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
    }
}