using AutoMapper;
using Identity.Data.Models;
using Identity.Interface.TransferObjects;
using OpenIddict.Models;

namespace Identity.Profiles
{
    public class IdentityProfile : Profile
    {
        public IdentityProfile() : base("Identity")
        {
            this.CreateMap<RegisterUser, ApplicationUser>().ReverseMap();
            this.CreateMap<User, ApplicationUser>().ReverseMap().ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id));

            this.CreateMap<Client, OpenIddictApplication>().ReverseMap();
        }
    }
}