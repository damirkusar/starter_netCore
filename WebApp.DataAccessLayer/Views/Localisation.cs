using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.DataAccessLayer.Views
{
    [Table("Localisations", Schema = "Facade")]
    public class Localisation
    {
        public string Key { get; set; }
        public string LanguageIsoAlpha2 { get; set; }
        public string Value { get; set; }
    }
}