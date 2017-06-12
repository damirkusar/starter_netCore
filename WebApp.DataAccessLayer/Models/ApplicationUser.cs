using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace WebApp.DataAccessLayer.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Image { get; set; }

        [NotMapped]
        public ICollection<string> AssignedRoles { get; set; }

        [NotMapped]
        public string GeneratedPassword { get; set; }
    }
}