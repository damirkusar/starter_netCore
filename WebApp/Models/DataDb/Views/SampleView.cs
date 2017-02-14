using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.Models.DataDb.Views
{
    [Table("Sample", Schema = "Facade")]
    public class SampleView
    {
        public string SampleProp { get; set; }
    }
}
