using Framework2.Domain.Core.Requests;
using MediatR;

namespace FlagService.Api.Entity.Flags
{
    public sealed class FlagEntityQueryRequest : FxEntityQueryRequest, IRequest<FlagEntityQueryResponse>
    {
    }
}
