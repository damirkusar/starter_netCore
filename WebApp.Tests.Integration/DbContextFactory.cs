using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApp.DataAccessLayer;

namespace WebApp.Tests.Integration
{
    public static class DbContextFactory
    {
        private static DataDbContext dataDbContextInstance;
        private static DataLayer dataLayerInstance;

        public static DataDbContext DataDbContextInstance
        {
            get
            {
                if (DbContextFactory.dataDbContextInstance == null)
                {
                    DbContextFactory.dataDbContextInstance = DbContextFactory.CreateDataDbContextInstance();
                }
                return DbContextFactory.dataDbContextInstance;
            }
        }

        public static DataLayer DataAccessLayerInstance
        {
            get
            {
                if (DbContextFactory.dataLayerInstance == null)
                {
                    DbContextFactory.dataLayerInstance = DbContextFactory.CreateDataAccessLayerInstance();
                }
                return DbContextFactory.dataLayerInstance;
            }
        }

        private static DataDbContext CreateDataDbContextInstance()
        {
            var serviceProvider = new ServiceCollection()
            .AddEntityFrameworkSqlServer()
            .BuildServiceProvider();
            var dbContextBuilder = new DbContextOptionsBuilder<DataDbContext>();
            dbContextBuilder.UseSqlServer($"Server=(localdb)\\mssqllocaldb;Database=WebAppSolution;Trusted_Connection=True;MultipleActiveResultSets=true")
                .UseInternalServiceProvider(serviceProvider);

            return new DataDbContext(dbContextBuilder.Options);
        }

        private static DataLayer CreateDataAccessLayerInstance()
        {
            return new DataLayer(DbContextFactory.DataDbContextInstance);
        }
    }
}
