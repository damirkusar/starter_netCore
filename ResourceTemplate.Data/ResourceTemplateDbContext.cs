using System;
using Microsoft.EntityFrameworkCore;
using ResourceTemplate.Data.Models;

namespace ResourceTemplate.Data
{
    public class ResourceTemplateDbContext : DbContext, IResourceTemplateDbContext
    {
        public DbSet<Resource> Resources { get; set; }

        public ResourceTemplateDbContext(DbContextOptions<ResourceTemplateDbContext> options): base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Resource>(c =>
            {
                c.Property(p => p.ResourceId).HasDefaultValue(Guid.NewGuid());
            });
        }
    }
}