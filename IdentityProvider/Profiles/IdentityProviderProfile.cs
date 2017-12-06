using AutoMapper;
using Identity.Interface.TransferObjects;
using IdentityProvider.TransferObjects;

namespace IdentityProvider.Profiles
{
    public class IdentityProviderProfile : Profile
    {
        public IdentityProviderProfile() : base("IdentityProvider")
        {
            this.CreateMap<RegisterUserRequest, RegisterUser>().ReverseMap();
            this.CreateMap<RegisterClientRequest, RegisterClient>().ReverseMap();
        }
    }
}