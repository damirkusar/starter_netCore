using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OpenIddict;

namespace Angular2Core.Models
{
    public class ApplicationDbContext : OpenIddictDbContext<ApplicationUser, ApplicationRole, string>
    {
        public ApplicationDbContext
            (DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            //nothing here
        }

    }
}