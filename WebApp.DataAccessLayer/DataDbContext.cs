using Microsoft.EntityFrameworkCore;
using WebApp.DataAccessLayer.Views;

namespace WebApp.DataAccessLayer
{
    public class DataDbContext : DbContext
    {
        // Models

        //Views
        public DbSet<Localisation> Localisations { get; set; }

        public DataDbContext(DbContextOptions<DataDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Models

            // Views
            modelBuilder.Entity<Localisation>().HasKey(l => new { l.Key, l.LanguageIsoAlpha2 });
        }
    }
}
