using Framework2.Domain.Core.Requests;
using MediatR;

namespace FlagService.Api.Features.Flags
{
    public class FlagToggleCommandRequest : FxCommandRequest, IRequest<FlagToggleCommandResponse>
    {
        public int FlagId { get; set; }
    }
}
