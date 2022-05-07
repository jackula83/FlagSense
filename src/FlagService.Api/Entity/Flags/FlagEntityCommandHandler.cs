using FlagService.Domain.Aggregates;
using FlagService.Domain.Aggregates.Flags;
using Framework2.Domain.Core.Handlers;
using Framework2.Domain.Core.Identity;
using Framework2.Infra.Data.Repository;
using MediatR;

namespace FlagService.Api.Entity.Flags
{
    public sealed class FlagEntityCommandHandler :
        FxEntityCommandHandler<FlagEntityCommandRequest, FlagEntityCommandResponse, Flag>,
        IRequestHandler<FlagEntityCommandRequest, FlagEntityCommandResponse>
    {
        public FlagEntityCommandHandler(IUserIdentity identity, ILogger<FlagEntityCommandHandler> logger, IFlagRepository repository) : base(identity, logger, repository)
        {
        }

        protected override bool HasPermission() => true;
    }
}
