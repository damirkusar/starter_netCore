using System;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Dal
{
    public partial class DataAccessLayer
    {
        public bool SetSomething(string id, int otherId)
        {
            var idParam = new SqlParameter("Id", id);
            var otherIdParam = new SqlParameter("OtherId", otherId);

            try
            {
                //var otherVersion = this.dataDbContext.Localizations.FromSql("EXECUTE Facade.SetSomething @Id, @OtherId", idParam, otherIdParam).ToList(); // this version needs to call for example toList() return the correct model from DB or create a dto.
                this.dataDbContext.Database.ExecuteSqlCommand("Facade.SetSomething @Id, @OtherId", idParam, otherIdParam);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
         }
    }
}
