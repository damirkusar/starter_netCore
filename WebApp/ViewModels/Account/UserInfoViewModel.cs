using System;
using System.Collections.Generic;
using WebApp.DataAccessLayer.Models;

namespace WebApp.ViewModels.Account
{
    public class UserInfoViewModel
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<string> AssignedRoles { get; set; }
        public string PhoneNumber { get; set; }

        public UserInfoViewModel(ApplicationUser user)
        {
            this.UserId = user.Id;
            this.Email = user.Email;
            this.FirstName = user.FirstName;
            this.LastName = user.LastName;
            this.PhoneNumber = user.PhoneNumber;
        }
    }
}
