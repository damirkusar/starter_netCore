using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WebApp.ViewModels.Account
{
    public class LogoutViewModel
    {
        [BindNever]
        public string RequestId { get; set; }
    }
}