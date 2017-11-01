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
        }
    }
}