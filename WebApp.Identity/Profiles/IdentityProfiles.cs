using AutoMapper;
using WebApp.Identity.DataAccessLayer.Models;
using WebApp.Identity.Interface.Models;

namespace WebApp.Identity.Profiles
{
    public class IdentityProfile : Profile
    {
        public IdentityProfile() : base("Identity")
        {
            this.CreateMap<RegisterUser, ApplicationUser>().ReverseMap();
        }
    }
}