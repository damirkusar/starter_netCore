using Microsoft.EntityFrameworkCore;
using WebApp.DataAccessLayer.Models;

namespace WebApp.DataAccessLayer
{
    public class DataDbContext : DbContext
    {
        // Models
        public DbSet<Localisation> Localisations { get; set; }

        //Views
        //public DbSet<SampleView> SampleView { get; set; }
        public DataDbContext(DbContextOptions<DataDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Models
            modelBuilder.Entity<Localisation>(entity =>
            {
                entity.ToTable("Localisations", "Model");
                entity.Property(e => e.LocalisationId).HasDefaultValueSql("newsequentialid()");
                entity.Property(e => e.Container).HasColumnType("varchar(255)");
                entity.Property(e => e.Key).IsRequired().HasColumnType("varchar(255)");
                entity.Property(e => e.Language).IsRequired().HasColumnType("varchar(255)");
                entity.Property(e => e.Value).IsRequired().HasColumnType("varchar(255)");
            });

            // Views
            //modelBuilder.Entity<SampleView>(entity => { entity.HasKey(e => e.SampleProp); });
        }
    }
}
