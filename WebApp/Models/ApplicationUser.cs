using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace WebApp.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}