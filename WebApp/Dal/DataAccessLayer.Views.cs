using System.Collections.Generic;
using WebApp.Models.DataDb.Views;

namespace WebApp.Dal
{
    public partial class DataAccessLayer
    {
        public IEnumerable<SampleView> GetSamples()
        {
            //var sample = this.dataDbContext.SampleView.ToList();
            //return sample;
            return null;
        }
    }
}
