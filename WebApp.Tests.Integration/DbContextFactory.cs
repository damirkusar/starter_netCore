using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApp.Dal;
using WebApp.Models.DataDb;

namespace WebApp.Tests.Integration
{
    public class DbContextFactory
    {
        private static DataAccessLayer dataAccessLayerInstance;

        public static DataAccessLayer DataAccessLayerInstance
        {
            get
            {
                if (DbContextFactory.dataAccessLayerInstance == null)
                {
                    DbContextFactory.dataAccessLayerInstance = DbContextFactory.CreateDataAccessLayerInstance();
                }
                return DbContextFactory.dataAccessLayerInstance;
            }
        }

        private static DataAccessLayer CreateDataAccessLayerInstance()
        {
            var serviceProvider = new ServiceCollection()
            .AddEntityFrameworkSqlServer()
            .BuildServiceProvider();
            var dbContextBuilder = new DbContextOptionsBuilder<DataDbContext>();
            dbContextBuilder.UseSqlServer($"Server=(localdb)\\mssqllocaldb;Database=WebAppSolution;Trusted_Connection=True;MultipleActiveResultSets=true")
                .UseInternalServiceProvider(serviceProvider);

            var dbContext = new DataDbContext(dbContextBuilder.Options);

            return new DataAccessLayer(dbContext);
        }
    }
}
