using FlagService.Domain.Aggregates;
using Framework2.Domain.Core.Responses;

namespace FlagService.Domain.Features.Flags.Commands
{
    public class FlagToggleCommandResponse : FxCommandResponse
    {
        public Flag? Flag { get; set; }
    }
}
