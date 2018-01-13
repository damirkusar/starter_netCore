using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AspNet.Security.OAuth.Introspection;
using AutoMapper;
using Common.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.SwaggerGen;
using WebApiGateway.TransferObjects;

namespace WebApiGateway.Controllers
{
    [Authorize(AuthenticationSchemes = OAuthIntrospectionDefaults.AuthenticationScheme)]
    [ApiExplorerSettings(IgnoreApi = false)]
    [Route("api/[controller]")]
    public class ResourceController : Controller
    {
        private readonly ILogger<ResourceController> logger;
        private readonly IMapper mapper;

        public ResourceController(
            ILogger<ResourceController> logger,
            IMapper mapper)
        {
            this.logger = logger;
            this.mapper = mapper;
        }

        [HttpGet]
        [ValidateModelState]
        //[SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(IList<Resource>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Type = typeof(ObjectResult))]
        public async Task<IActionResult> Get()
        {
            //var resources = await this.resourceService.LoadAsync();
            //return this.Ok(resources);
            return this.NoContent();
        }

        [HttpGet("{resourceId}")]
        [ValidateModelState]
        //[SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(Resource))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Type = typeof(ObjectResult))]
        public async Task<IActionResult> Get(Guid resourceId)
        {
            //var resource = await this.resourceService.LoadAsync(resourceId);
            //return this.Ok(resource);
            return this.NoContent();
        }

        [HttpPost]
        [ValidateModelState]
        //[SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(Resource))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Type = typeof(ObjectResult))]
        public async Task<IActionResult> Register([FromBody] ResourceRequest request)
        {
            //var newResource = this.mapper.Map<ResourceRequest, Resource>(request);
            //var result = await this.resourceService.AddAsync(newResource);
            //if (result == null)
            //{
            //    return this.StatusCode((int)HttpStatusCode.InternalServerError);
            //}

            //return this.Ok(result);
            return this.NoContent();
        }
    }
}