using Framework2.Domain.Core.Requests;
using Framework2.Infra.Data.Entity;

namespace FlagService.Api.Fx
{
    public sealed class BlankCommandRequest<TAggregateRoot> : FxEntityCommandRequest<TAggregateRoot>
        where TAggregateRoot : IAggregateRoot
    {
    }
}
