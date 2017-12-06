using System;
using Identity.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Identity.Data
{
    public class IdentityDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>, IIdentityDbContext
    {
        public IdentityDbContext(DbContextOptions<IdentityDbContext> options): base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>(c =>
            {
                c.Property(p => p.LockoutEnabled).HasDefaultValue(true);
            });
        }
    }
}