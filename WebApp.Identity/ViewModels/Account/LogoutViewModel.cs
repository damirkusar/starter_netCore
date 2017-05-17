using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WebApp.Identity.ViewModels.Account
{
    public class LogoutViewModel
    {
        [BindNever]
        public string RequestId { get; set; }
    }
}