using AutoMapper;
using Identity.Data.Models;
using Identity.Interface.Models;

namespace Identity.Profiles
{
    public class IdentityProfile : Profile
    {
        public IdentityProfile() : base("Identity")
        {
            this.CreateMap<RegisterUser, ApplicationUser>().ReverseMap();
        }
    }
}