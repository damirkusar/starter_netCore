using System.ComponentModel.DataAnnotations;

namespace WebApp.Identity.ViewModels.Account
{
    public class ForceChangePasswordViewModel
    {
        //[DataType(DataType.EmailAddress)]
        //public string Email { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}