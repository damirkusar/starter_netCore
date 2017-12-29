using AutoMapper;

namespace ResourceTemplate.Profiles
{
    public class ResourceProfile : Profile
    {
        public ResourceProfile() : base("Resource")
        {
            this.CreateMap<Interface.TransferObjects.Resource, Data.Models.Resource>().ReverseMap();
        }
    }
}