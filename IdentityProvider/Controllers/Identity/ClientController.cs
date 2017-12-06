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
    [AllowAnonymous]
    [ApiExplorerSettings(IgnoreApi = false)]
    [Route("api/[controller]")]
    public class ClientController : Controller
    {
        private readonly ILogger<ClientController> logger;
        private readonly IMapper mapper;
        private readonly IRegisterClient registerClient;
        private readonly IDeleteClient deleteClient;

        public ClientController(
            ILogger<ClientController> logger,
            IMapper mapper,
            IRegisterClient registerClient,
            IDeleteClient deleteClient)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.registerClient = registerClient;
            this.deleteClient = deleteClient;
        }

        [HttpPost]
        [ValidateModelState]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(NoContentResult))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Type = typeof(ObjectResult))]
        public async Task<IActionResult> Register([FromBody] RegisterClientRequest request)
        {
            var client = this.mapper.Map<RegisterClientRequest, RegisterClient>(request);
            await this.registerClient.RegisterAsync(client);
            return this.NoContent();
        }

        [HttpDelete("{clientId}")]
        [ValidateModelState]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(NoContentResult))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Type = typeof(ObjectResult))]
        public async Task<IActionResult> Delete(string clientId)
        {
            await this.deleteClient.DeleteAsync(clientId);
            return this.NoContent();
        }
    }
}