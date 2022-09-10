using FlagService.Domain.Aggregates;
using Framework2.Domain.Core.Responses;

namespace FlagService.Api.Features.Flags
{
    public class FlagToggleCommandResponse : FxCommandResponse
    {
        public Flag? Flag { get; set; }
    }
}
