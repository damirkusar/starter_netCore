using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace WebApp.DataAccessLayer.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}