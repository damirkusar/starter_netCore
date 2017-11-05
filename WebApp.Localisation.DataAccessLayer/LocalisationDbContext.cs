using Microsoft.EntityFrameworkCore;

namespace WebApp.Localisation.DataAccessLayer
{
    public class LocalisationDbContext : DbContext, ILocalisationDbContext
    {
        public DbSet<Models.Localisation> Localisations { get; set; }

        public LocalisationDbContext(DbContextOptions<LocalisationDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("Model");
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Models.Localisation>(c =>
            {
                c.ToTable("Localisations");
                c.Property(p => p.LocalisationId).HasDefaultValueSql("NEWSEQUENTIALID()");
            });
        }

    }
}
