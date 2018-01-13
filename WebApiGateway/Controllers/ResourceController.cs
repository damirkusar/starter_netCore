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
using Microsoft.Extensions.Options;
using RestSharp;
using Swashbuckle.AspNetCore.SwaggerGen;
using WebApiGateway.Adaptor;
using WebApiGateway.Settings;
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
        private readonly MicroserviceUrls microserviceUrls;
        private readonly ICustomRestClient restClient;

        public ResourceController(
            ILogger<ResourceController> logger,
            IMapper mapper,
            IOptions<MicroserviceUrls> microserviceUrls,
            ICustomRestClient restClient)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.microserviceUrls = microserviceUrls.Value;
            this.restClient = restClient;
        }

        [HttpGet]
        [ValidateModelState]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(IList<ResourceResponse>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Type = typeof(ObjectResult))]
        public async Task<IActionResult> Get()
        {
            var request = this.restClient.CreateRequest(this.HttpContext, this.microserviceUrls.ResourceService, "resource", Method.GET);

            var response = await this.restClient.ExecuteTaskAsync<List<ResourceResponse>>(request);

            if (!response.IsSuccessful)
            {
                return this.StatusCode((int)response.StatusCode);
            }

            var resource = response.Data;
            return this.Ok(resource);
        }

        [HttpGet("{resourceId}")]
        [ValidateModelState]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ResourceResponse))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Type = typeof(ObjectResult))]
        public async Task<IActionResult> Get(Guid resourceId)
        {
            var request = this.restClient.CreateRequest(this.HttpContext, this.microserviceUrls.ResourceService, $"resource/{resourceId}", Method.GET);

            var response = await this.restClient.ExecuteTaskAsync<ResourceResponse>(request);

            if (!response.IsSuccessful)
            {
                return this.StatusCode((int)response.StatusCode);
            }

            var resource = response.Data;
            return this.Ok(resource);
        }

        [HttpPost]
        [ValidateModelState]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ResourceResponse))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Type = typeof(ObjectResult))]
        public async Task<IActionResult> Post([FromBody] ResourceRequest resourceRequest)
        {
            var request = this.restClient.CreateRequest(this.HttpContext, this.microserviceUrls.ResourceService, $"resource", Method.POST);
            request.AddJsonBody(resourceRequest);

            var response = await this.restClient.ExecuteTaskAsync<ResourceResponse>(request);

            if (!response.IsSuccessful)
            {
                return this.StatusCode((int) response.StatusCode);
            }

            var resource = response.Data;
            return this.Ok(resource);
        }
    }
}