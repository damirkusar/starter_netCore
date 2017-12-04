using System.ComponentModel.DataAnnotations;

namespace IdentityProvider.TransferObjects
{
    public class RegisterClientRequest
    {
        [Required]
        public string ClientId { get; set; }
        [Required]
        public string ClientSecret { get; set; }
        [Required]
        public string DisplayName { get; set; }
    }
}