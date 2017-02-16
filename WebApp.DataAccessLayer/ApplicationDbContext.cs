using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApp.DataAccessLayer.Models;

namespace WebApp.DataAccessLayer
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            //nothing here
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultSchema("SecurityModel");
            base.OnModelCreating(builder);

            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            builder.Entity<IdentityRoleClaim<Guid>>(roleClaims =>
            {
                roleClaims.ToTable("RoleClaims");
                //roleClaims.Property(p => p.Id).HasDefaultValueSql("NEWID()").HasColumnName("RoleClaimId").IsRequired(true);
                roleClaims.Property(p => p.ClaimType).HasColumnType("varchar(256)").HasColumnName("Type").IsRequired(true);
                roleClaims.Property(p => p.ClaimValue).HasColumnType("varchar(256)").HasColumnName("Value").IsRequired(true);
            });
            builder.Entity<ApplicationRole>(roles =>
            {
                roles.ToTable("Roles");
                roles.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()").HasColumnName("RoleId").IsRequired(true);
                roles.Property(p => p.Name).HasColumnType("varchar(64)").IsRequired(true);
                roles.Property(p => p.NormalizedName).HasColumnType("varchar(64)");
            });
            builder.Entity<IdentityUserClaim<Guid>>(userClaims =>
            {
                userClaims.ToTable("UserClaims");
                userClaims.Property(p => p.ClaimType).HasColumnType("varchar(256)").HasColumnName("Type").IsRequired(true);
                userClaims.Property(p => p.ClaimValue).HasColumnType("varchar(256)").HasColumnName("Value").IsRequired(true);
            });
            builder.Entity<IdentityUserLogin<Guid>>(userLogins =>
            {
                userLogins.ToTable("UserLogins");
                userLogins.Property(p => p.LoginProvider).HasColumnType("varchar(256)").IsRequired(true);
                userLogins.Property(p => p.ProviderKey).HasColumnType("varchar(256)").IsRequired(true);
                userLogins.Property(p => p.ProviderDisplayName).HasColumnType("varchar(256)");
            });
            builder.Entity<IdentityUserLogin<Guid>>().ToTable("UserLogins");
            builder.Entity<IdentityUserRole<Guid>>().ToTable("UserRoles");
            builder.Entity<ApplicationUser>(users =>
            {
                users.ToTable("Users");
                users.Property(p => p.Id).HasColumnName("UserId");
                users.Property(p => p.Email).HasColumnType("varchar(320)").IsRequired(true);
                users.Property(p => p.FirstName).HasColumnType("varchar(64)").IsRequired(true);
                users.Property(p => p.LastName).HasColumnType("varchar(64)").IsRequired(true);
                users.Property(p => p.NormalizedEmail).HasColumnType("varchar(320)");
                users.Property(p => p.NormalizedUserName).HasColumnType("varchar(320)");
                users.Property(p => p.PhoneNumber).HasColumnType("varchar(32)");
                users.Property(p => p.UserName).HasColumnType("varchar(320)").IsRequired(true);
            });
            builder.Entity<IdentityUserToken<Guid>>().ToTable("UserTokens");
        }
    }
}