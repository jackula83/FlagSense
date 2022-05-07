using FlagService.Api.Fx;
using FlagService.Domain.Aggregates;
using Framework2.Application.Core.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FlagService.Api.Entity.Flags
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlagEntityController : FxEntityController<FlagEntityQueryRequest, BlankCommandRequest<Flag>, Flag>
    {
        public FlagEntityController(IMediator mediator, ILogger<FlagEntityController> logger) : base(mediator, logger)
        {
        }

#pragma warning disable 
        public override async Task<IActionResult> Post([FromBody] BlankCommandRequest<Flag> request) => BadRequest();
        public override async Task<IActionResult> Delete([FromBody] BlankCommandRequest<Flag> request) => BadRequest();
#pragma warning restore
    }
}
