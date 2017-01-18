using Microsoft.EntityFrameworkCore;

namespace Angular2Core.Models.DataDb
{
    public class DataDbContext : DbContext
    {
        public DbSet<Localization> Localizations { get; set; }
        public DataDbContext(DbContextOptions<DataDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Localization>().ToTable("Localization");
        }
    }
}
