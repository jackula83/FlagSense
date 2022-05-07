using FlagService.Domain.Aggregates;
using FlagService.Domain.Aggregates.Flags;
using Framework2.Domain.Core.Handlers;
using Framework2.Infra.Data.Repository;
using MediatR;

namespace FlagService.Api.Entity.Flags
{
    public sealed class FlagEntityQueryHandler : 
        FxEntityQueryHandler<FlagEntityQueryRequest, FlagEntityQueryResponse, Flag>,
        IRequestHandler<FlagEntityQueryRequest, FlagEntityQueryResponse>
    {
        public FlagEntityQueryHandler(ILogger<FlagEntityQueryHandler> logger, IFlagRepository repository) : base(logger, repository)
        {
        }
    }
}
