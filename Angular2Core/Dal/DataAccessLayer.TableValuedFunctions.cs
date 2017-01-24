using System.Collections.Generic;
using Angular2Core.Models.DataDb;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Angular2Core.Dal
{
    public partial class DataAccessLayer
    {
        public List<Localization> GetLocalizationsViaTableValuedFunction(string language)
        {
            return this.dataDbContext.Localizations.FromSql("SELECT * FROM Facade.Localizations({0})", language).OrderByDescending(p => p.Container).ToList();
        }
    }
}
