using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AspNet.Security.OAuth.Introspection;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ResourceProvider.Filters;
using ResourceProvider.TransferObjects;
using ResourceTemplate.Interface.Services;
using ResourceTemplate.Interface.TransferObjects;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ResourceProvider.Controllers
{
    [Authorize(AuthenticationSchemes = OAuthIntrospectionDefaults.AuthenticationScheme)]
    [ApiExplorerSettings(IgnoreApi = false)]
    [Route("api/[controller]")]
    public class ResourceController : Controller
    {
        private readonly ILogger<ResourceController> logger;
        private readonly IMapper mapper;
        private readonly IResourceService resourceService;

        public ResourceController(
            ILogger<ResourceController> logger,
            IMapper mapper,
            IResourceService resourceService)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.resourceService = resourceService;
        }

        [HttpGet]
        [ValidateModelState]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(IList<Resource>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Type = typeof(ObjectResult))]
        public async Task<IActionResult> Get()
        {
            var resources = await this.resourceService.LoadAsync();
            return this.Ok(resources);
        }

        [HttpGet("{resourceId}")]
        [ValidateModelState]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(Resource))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Type = typeof(ObjectResult))]
        public async Task<IActionResult> Get(Guid resourceId)
        {
            var resource = await this.resourceService.LoadAsync(resourceId);
            return this.Ok(resource);
        }

        [HttpPost]
        [ValidateModelState]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(Resource))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Type = typeof(ObjectResult))]
        public async Task<IActionResult> Register([FromBody] ResourceRequest request)
        {
            var newResource = this.mapper.Map<ResourceRequest, Resource>(request);
            var result = await this.resourceService.AddAsync(newResource);
            if (result == null)
            {
                return this.StatusCode((int)HttpStatusCode.InternalServerError);
            }

            return this.Ok(result);
        }
    }
}