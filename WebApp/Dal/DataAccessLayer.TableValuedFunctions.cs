using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebApp.Models.DataDb;

namespace WebApp.Dal
{
    public partial class DataAccessLayer
    {
        public List<Localisation> GetLocalizationsViaTableValuedFunction(string language)
        {
            return this.dataDbContext.Localizations.FromSql("SELECT * FROM Facade.Localisations({0})", language).OrderByDescending(p => p.Container).ToList();
        }
    }
}
