using FlagService.Domain.Aggregates;
using Framework2.Domain.Core.Requests;
using MediatR;

namespace FlagService.Api.Entity.Flags
{
    public sealed class FlagEntityCommandRequest : FxEntityCommandRequest<Flag>, IRequest<FlagEntityCommandResponse>
    {
    }
}
