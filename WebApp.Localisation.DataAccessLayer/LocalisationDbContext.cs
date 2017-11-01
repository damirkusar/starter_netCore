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

            modelBuilder.Entity<Models.Localisation>(l =>
            {
                l.ToTable("Localisations");
                l.Property(p => p.LocalisationId).HasDefaultValueSql("NEWSEQUENTIALID()");
                // l.HasKey(item => new { item.LocalisationId });
            });
        }

    }
}
