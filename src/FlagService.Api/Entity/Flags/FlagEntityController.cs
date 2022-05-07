using FlagService.Domain.Aggregates;
using Framework2.Application.Core.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FlagService.Api.Entity.Flags
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlagEntityController : FxEntityController<FlagEntityQueryRequest, FlagEntityCommandRequest, Flag>
    {
        public FlagEntityController(IMediator mediator, ILogger<FlagEntityController> logger) : base(mediator, logger)
        {
        }
    }
}
