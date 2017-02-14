using WebApp.Models.DataDb;

namespace WebApp.Dal
{
    public partial class DataAccessLayer
    {
        private readonly DataDbContext dataDbContext;

        public DataAccessLayer(DataDbContext dataDbContext)
        {
            this.dataDbContext = dataDbContext;
        }
    }
}
