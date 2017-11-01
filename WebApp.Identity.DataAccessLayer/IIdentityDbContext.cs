using Microsoft.EntityFrameworkCore;
using WebApp.Identity.DataAccessLayer.Models;

namespace WebApp.Identity.DataAccessLayer
{
    public interface IIdentityDbContext
    {
        DbSet<ApplicationUser> Users { get; set; }
        DbSet<ApplicationRole> Roles { get; set; }
    }
}