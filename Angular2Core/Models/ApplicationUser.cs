using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Angular2Core.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}