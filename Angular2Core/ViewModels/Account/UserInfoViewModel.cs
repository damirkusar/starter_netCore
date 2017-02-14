using System.Collections.Generic;
using WebApp.Models;

namespace WebApp.ViewModels.Account
{
    public class UserInfoViewModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        //public ICollection<IdentityUserRole<string>> UserRoles { get; set; }
        public ICollection<string> AssignedRoles { get; set; }
        public string PhoneNumber { get; set; }

        public UserInfoViewModel(ApplicationUser user)
        {
            this.Id = user.Id;
            this.Email = user.Email;
            this.FirstName = user.FirstName;
            this.LastName = user.LastName;
            this.PhoneNumber = user.PhoneNumber;
            //this.Roles = user.Roles;
        }
    }
}
