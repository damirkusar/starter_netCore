using System.Collections.Generic;
using Angular2Core.Models.DataDb.Views;
using System.Linq;

namespace Angular2Core.Dal
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
