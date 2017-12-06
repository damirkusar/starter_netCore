using System.ComponentModel.DataAnnotations;

namespace IdentityProvider.TransferObjects
{
    public class ClientRequest
    {
        [Required]
        public string ClientId { get; set; }
        [Required]
        public string ClientSecret { get; set; }
        [Required]
        public string DisplayName { get; set; }
    }
}