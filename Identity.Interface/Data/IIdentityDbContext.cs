using Identity.Interface.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Identity.Interface.Data
{
    public interface IIdentityDbContext
    {
        DbSet<ApplicationUser> Users { get; set; }
        DbSet<ApplicationRole> Roles { get; set; }
    }
}