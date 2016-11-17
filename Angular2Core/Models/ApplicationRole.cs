using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Angular2Core.Models
{
    public class ApplicationRole : IdentityRole
    {
        public string Description { get; set; }
    }
}