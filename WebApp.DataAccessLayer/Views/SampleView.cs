using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.DataAccessLayer.Views
{
    [Table("Sample", Schema = "Facade")]
    public class SampleView
    {
        public string SampleProp { get; set; }
    }
}
