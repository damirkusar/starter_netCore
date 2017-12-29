using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Identity.Interface.Services;
using Identity.Interface.TransferObjects;
using IdentityProvider.Filters;
using IdentityProvider.TransferObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace IdentityProvider.Controllers.Identity
{
    [Authorize]
    [ApiExplorerSettings(IgnoreApi = false)]
    [Route("api/[controller]")]
    public class ClientController : Controller
    {
        private readonly ILogger<ClientController> logger;
        private readonly IMapper mapper;
        private readonly IClientService clientService;

        public ClientController(
            ILogger<ClientController> logger,
            IMapper mapper,
            IClientService clientService)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.clientService = clientService;
        }

        [HttpGet("{clientId}")]
        [ValidateModelState]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(Client))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Type = typeof(ObjectResult))]
        public async Task<IActionResult> GetClient(string clientId)
        {
            var client = await this.clientService.LoadAsync(clientId);
            if (client == null)
            {
                return this.BadRequest();
            }

            return this.Ok(client);
        }

        [HttpPost]
        [ValidateModelState]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(NoContentResult))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Type = typeof(ObjectResult))]
        public async Task<IActionResult> Register([FromBody] ClientRequest request)
        {
            var client = this.mapper.Map<ClientRequest, Client>(request);
            await this.clientService.RegisterAsync(client);
            return this.NoContent();
        }

        [HttpPut("{clientId}")]
        [ValidateModelState]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(NoContentResult))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Type = typeof(ObjectResult))]
        public async Task<IActionResult> Update(string clientId, [FromBody] ClientRequest request)
        {
            var client = this.mapper.Map<ClientRequest, Client>(request);
            await this.clientService.UpdateAsync(client);
            return this.NoContent();
        }

        [HttpDelete("{clientId}")]
        [ValidateModelState]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(NoContentResult))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Type = typeof(ObjectResult))]
        public async Task<IActionResult> Delete(string clientId)
        {
            await this.clientService.DeleteAsync(clientId);
            return this.NoContent();
        }
    }
}