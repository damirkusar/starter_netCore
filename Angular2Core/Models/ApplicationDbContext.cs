using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Angular2Core.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public ApplicationDbContext
            (DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            //nothing here
        }

    }
}