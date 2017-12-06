using Identity.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Identity.Data
{
    public interface IIdentityDbContext
    {
        DbSet<ApplicationUser> Users { get; set; }
        DbSet<ApplicationRole> Roles { get; set; }
    }
}