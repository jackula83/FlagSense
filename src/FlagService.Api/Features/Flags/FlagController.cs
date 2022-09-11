using FlagService.Core.Extensions;
using Framework2.Application.Core.Controllers;
using Framework2.Core.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FlagService.Api.Features.Flags
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlagController : FxController
    {
        public FlagController(IMediator mediator, ILogger<FlagController> logger) : base(mediator, logger)
        {
        }

        [HttpPost("toggle/")]
        public async Task<IActionResult> ToggleFlag([FromBody] FlagControllerToggleRequest request)
        {
            var command = request
                .CreateRequest<FlagToggleCommandRequest>()
                .Tap(x => x.FlagId = request.FlagId);
            return await this.Send(command);
        }
    }
}
