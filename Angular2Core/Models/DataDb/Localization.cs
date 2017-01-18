using System.ComponentModel.DataAnnotations;

namespace Angular2Core.Models.DataDb
{
    public class Localization
    {
        public int Id { get; set; }
        public string Container { get; set; }
        [Required]
        public string Language { get; set; }
        [Required]
        public string Key { get; set; }
        [Required]
        public string Value { get; set; }
    }
}
