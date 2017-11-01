using Microsoft.EntityFrameworkCore;

namespace WebApp.Localisation.DataAccessLayer
{
    public interface ILocalisationDbContext
    {
        DbSet<Models.Localisation> Localisations { get; set; }
    }
}