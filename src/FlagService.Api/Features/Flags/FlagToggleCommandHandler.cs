using FlagService.Domain.Aggregates.Flags;
using Framework2.Core.Extensions;
using Framework2.Domain.Core.Handlers;
using MediatR;

namespace FlagService.Api.Features.Flags
{
    public class FlagToggleCommandHandler : 
        FxCommandHandler<FlagToggleCommandRequest, FlagToggleCommandResponse>, 
        IRequestHandler<FlagToggleCommandRequest, FlagToggleCommandResponse>
    {
        private readonly IFlagRepository _flagRepository;

        public FlagToggleCommandHandler(IFlagRepository flagRepository, ILogger<FlagToggleCommandHandler> logger)
            : base(logger)
        {
            _flagRepository = flagRepository;
        }

        protected override async Task<FlagToggleCommandResponse> ExecuteAsync(FlagToggleCommandRequest request, CancellationToken cancellationToken = default)
        {
            var flag = await _flagRepository.Get(request.FlagId);
            if (flag == null)
                return new();

            flag.Tap(x => x.IsEnabled = !x.IsEnabled);
            await _flagRepository.Update(flag);
            await _flagRepository.Save();

            return new FlagToggleCommandResponse { Flag = flag };
        }
    }
}
