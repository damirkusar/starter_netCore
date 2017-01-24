using Angular2Core.Models.DataDb.Views;
using Microsoft.EntityFrameworkCore;

namespace Angular2Core.Models.DataDb
{
    public class DataDbContext : DbContext
    {
        // Models
        public DbSet<Localizations> Localizations { get; set; }

        //Views
        //public DbSet<SampleView> SampleView { get; set; }
        public DataDbContext(DbContextOptions<DataDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Models
            modelBuilder.Entity<Localizations>(entity =>
            {
                entity.ToTable("Localizations", "Model");
                entity.Property(e => e.Id).HasDefaultValueSql("newsequentialid()");
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
