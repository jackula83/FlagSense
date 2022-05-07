using Framework2.Domain.Core.Identity;

namespace FlagService.Api.Entity.Identity
{
    public sealed class UserIdentity : IUserIdentity
    {
        public string UserName { get; set; } = "free-version";
    }
}
