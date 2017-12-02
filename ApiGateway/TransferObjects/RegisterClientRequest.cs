using System.ComponentModel.DataAnnotations;

namespace ApiGateway.TransferObjects
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