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
            this.CreateMap<UpdateUserRequest, UpdatedUser>().ReverseMap();
            this.CreateMap<ClientRequest, Client>().ReverseMap();
        }
    }
}