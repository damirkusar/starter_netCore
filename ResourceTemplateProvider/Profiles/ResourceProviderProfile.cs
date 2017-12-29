using AutoMapper;
using ResourceProvider.TransferObjects;
using ResourceTemplate.Interface.TransferObjects;

namespace ResourceProvider.Profiles
{
    public class ResourceProviderProfile : Profile
    {
        public ResourceProviderProfile() : base("ResourceProvider")
        {
            this.CreateMap<ResourceRequest, Resource>().ReverseMap();
        }
    }
}