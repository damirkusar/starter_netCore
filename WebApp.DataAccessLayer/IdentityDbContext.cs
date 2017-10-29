using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApp.DataAccessLayer.Models;

namespace WebApp.DataAccessLayer
{
    public class IdentityDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public IdentityDbContext(DbContextOptions<IdentityDbContext> options): base(options)
        {
            //nothing here
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultSchema("Identiy");
            base.OnModelCreating(builder);

            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            //builder.Entity<IdentityRoleClaim<Guid>>().ToTable("RoleClaims");
            //builder.Entity<IdentityUserClaim<Guid>>().ToTable("UserClaims");
            //builder.Entity<IdentityUserLogin<Guid>>().ToTable("UserLogins");
            //builder.Entity<IdentityUserLogin<Guid>>().ToTable("UserLogins");
            //builder.Entity<IdentityUserRole<Guid>>().ToTable("UserRoles");
            //builder.Entity<IdentityUserToken<Guid>>().ToTable("UserTokens");
            //builder.Entity<ApplicationUser>(users =>
            //{
            //    users.ToTable("Users");
            //    users.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()").HasColumnName("UserId");
            //});
            //builder.Entity<ApplicationRole>(roles =>
            //{
            //    roles.ToTable("Roles");
            //    roles.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()").HasColumnName("RoleId");
            //});
        }
    }
}