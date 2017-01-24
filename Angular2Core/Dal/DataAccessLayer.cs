using Angular2Core.Models.DataDb;

namespace Angular2Core.Dal
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
