using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResourceTemplate.Interface.TransferObjects;

namespace ResourceTemplate.Interface.Services
{
    public interface IResourceService
    {
        Task<Resource> AddAsync(Resource resource);
        Task<IList<Resource>> LoadAsync();
        Task<Resource> LoadAsync(Guid resourceId);
    }
}