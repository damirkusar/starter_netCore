using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Angular2Core.ViewModels.Account
{
    public class LogoutViewModel
    {
        [BindNever]
        public string RequestId { get; set; }
    }
}