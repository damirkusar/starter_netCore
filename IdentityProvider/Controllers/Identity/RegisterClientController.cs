using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Identity.Interface;
using Identity.Interface.Models;
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
    public class RegisterClientController : Controller
    {
        private readonly ILogger<RegisterClientController> logger;
        private readonly IMapper mapper;
        private readonly IRegisterClientService registerService;

        public RegisterClientController(
            ILogger<RegisterClientController> logger,
            IMapper mapper,
            IRegisterClientService registerService)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.registerService = registerService;
        }

        [HttpPost]
        [ValidateModelState]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(NoContentResult))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Type = typeof(ObjectResult))]
        public async Task<IActionResult> Register([FromBody] RegisterClientRequest request)
        {
            var client = this.mapper.Map<RegisterClientRequest, RegisterClient>(request);
            await this.registerService.RegisterAsync(client);

            return this.NoContent();
        }
    }
}