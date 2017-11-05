using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApp.Identity.DataAccessLayer.Models;

namespace WebApp.Identity.DataAccessLayer
{
    public class IdentityDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>, IIdentityDbContext
    {
        public IdentityDbContext(DbContextOptions<IdentityDbContext> options): base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultSchema("Identiy");
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>(c =>
            {
                c.Property(p => p.LockoutEnabled).HasDefaultValue(true);
            });
        }
    }
}