namespace WebApp.DataAccessLayer
{
    public partial class DataLayer
    {
        private readonly DataDbContext dataDbContext;

        public DataLayer(DataDbContext dataDbContext)
        {
            this.dataDbContext = dataDbContext;
        }
    }
}
