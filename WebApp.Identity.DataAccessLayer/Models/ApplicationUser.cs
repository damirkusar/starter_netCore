using System;
using Microsoft.AspNetCore.Identity;

namespace WebApp.Identity.DataAccessLayer.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}