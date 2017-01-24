using System.ComponentModel.DataAnnotations.Schema;

namespace Angular2Core.Models.DataDb.Views
{
    [Table("Sample", Schema = "Facade")]
    public class SampleView
    {
        public string SampleProp { get; set; }
    }
}
