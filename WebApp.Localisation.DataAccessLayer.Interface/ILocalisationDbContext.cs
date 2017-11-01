using Microsoft.EntityFrameworkCore;

namespace WebApp.Localisation.DataAccessLayer.Interface
{
    public interface ILocalisationDbContext
    {
        DbSet<Models.Localisation> Localisations { get; set; }
    }
}