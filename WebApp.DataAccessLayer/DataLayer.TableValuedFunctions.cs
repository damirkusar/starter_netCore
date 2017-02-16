using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebApp.DataAccessLayer.Models;

namespace WebApp.DataAccessLayer
{
    public partial class DataLayer
    {
        public List<Localisation> GetLocalizationsViaTableValuedFunction(string language)
        {
            return this.dataDbContext.Localizations.FromSql("SELECT * FROM Facade.Localisations({0})", language).OrderByDescending(p => p.Container).ToList();
        }
    }
}
