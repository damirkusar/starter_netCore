using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ResourceTemplate.Data;
using ResourceTemplate.Interface.Services;
using ResourceTemplate.Interface.TransferObjects;

namespace ResourceTemplate.Services
{
    public class ResourceService : IResourceService
    {
        private readonly ILogger<ResourceService> logger;
        private readonly IMapper mapper;
        private readonly IResourceTemplateDbContext resourceTemplateDbContext;

        public ResourceService(
            ILogger<ResourceService> logger,
            IMapper mapper,
            IResourceTemplateDbContext resourceTemplateDbContext
            )
        {
            this.logger = logger;
            this.mapper = mapper;
            this.resourceTemplateDbContext = resourceTemplateDbContext;
        }

        public async Task<Resource> AddAsync(Resource resource)
        {
            var resourceModel = this.mapper.Map<Resource, Data.Models.Resource>(resource);
            var resourceModelAdded = await this.resourceTemplateDbContext.Resources.AddAsync(resourceModel);
            await this.resourceTemplateDbContext.SaveChangesAsync();

            var resourceDto = this.mapper.Map<Data.Models.Resource, Resource>(resourceModelAdded.Entity);
            return resourceDto;
        }

        public async Task<IList<Resource>> LoadAsync()
        {
            var resources = await this.resourceTemplateDbContext.Resources.ToListAsync();
            var resourceDtos = this.mapper.Map<IList<Data.Models.Resource>, IList<Resource>>(resources);
            return resourceDtos;
        }

        public async Task<Resource> LoadAsync(Guid resourceId)
        {
            var resource = await this.resourceTemplateDbContext.Resources.FirstOrDefaultAsync(x => x.ResourceId == resourceId);
            if (resource == null)
            {
                return null;
            }

            var resourceDto = this.mapper.Map<Data.Models.Resource, Resource>(resource);
            return resourceDto;
        }
    }
}