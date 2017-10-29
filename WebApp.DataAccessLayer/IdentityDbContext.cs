using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApp.DataAccessLayer.Models;

namespace WebApp.DataAccessLayer
{
    public class IdDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public IdDbContext(DbContextOptions<IdDbContext> options): base(options)
        {
            //nothing here
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultSchema("Identity");
            base.OnModelCreating(builder);

            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            //builder.Entity<IdentityRoleClaim<Guid>>(roleClaims =>
            //{
            //    roleClaims.ToTable("RoleClaims");
            //});
            //builder.Entity<ApplicationRole>(roles =>
            //{
            //    roles.ToTable("Roles");
            //});
            //builder.Entity<IdentityUserClaim<Guid>>(userClaims =>
            //{
            //    userClaims.ToTable("UserClaims");
            //});
            //builder.Entity<IdentityUserLogin<Guid>>(userLogins =>
            //{
            //    userLogins.ToTable("UserLogins");
            //});
            //builder.Entity<IdentityUserLogin<Guid>>().ToTable("UserLogins");
            //builder.Entity<IdentityUserRole<Guid>>().ToTable("UserRoles");
            //builder.Entity<ApplicationUser>(users =>
            //{
            //    users.ToTable("Users");
            //});
            //builder.Entity<IdentityUserToken<Guid>>().ToTable("UserTokens");
        }
    }
}