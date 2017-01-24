using Angular2Core.Models.DataDb.Views;
using Microsoft.EntityFrameworkCore;

namespace Angular2Core.Models.DataDb
{
    public class DataDbContext : DbContext
    {
        // Models
        public DbSet<Localization> Localizations { get; set; }

        //Views
        public DbSet<SampleView> SampleView { get; set; }
        public DataDbContext(DbContextOptions<DataDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Models
            modelBuilder.Entity<Localization>().ToTable("Localization");

            // Views
            modelBuilder.Entity<SampleView>(entity => { entity.HasKey(e => e.SampleProp); });
        }
    }
}
