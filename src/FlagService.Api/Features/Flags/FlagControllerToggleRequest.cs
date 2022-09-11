using Framework2.Application.Core.Requests;

namespace FlagService.Api.Features.Flags
{
    public class FlagControllerToggleRequest : FxControllerRequest
    {
        public int FlagId { get; set; }
    }
}
