using System;
using System.ComponentModel.DataAnnotations;

namespace WebApp.DataAccessLayer.Models
{
    public class Localisation
    {
        [Key]
        public Guid LocalisationId { get; set; }
        public string Container { get; set; }
        [Required]
        public string Language { get; set; }
        [Required]
        public string Key { get; set; }
        [Required]
        public string Value { get; set; }
    }
}
