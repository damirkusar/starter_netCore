using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace WebApp.Models
{
    public class ApplicationRole : IdentityRole
    {
        public string Description { get; set; }
    }
}