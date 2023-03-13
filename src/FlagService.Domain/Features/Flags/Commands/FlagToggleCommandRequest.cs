using Framework2.Domain.Core.Requests;
using MediatR;

namespace FlagService.Domain.Features.Flags.Commands
{
    public class FlagToggleCommandRequest : FxCommandRequest, IRequest<FlagToggleCommandResponse>
    {
        public int FlagId { get; set; }
    }
}
