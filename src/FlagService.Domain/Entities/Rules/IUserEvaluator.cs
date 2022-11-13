using FlagService.Domain.Aggregates;
using FlagService.Domain.Aggregates.Users;

namespace FlagService.Domain.Entities.Rules
{
    public interface IUserEvaluator
    {
        bool EvaluateUserAttributes(User user);
        bool EvaluateAttributes(List<UserAttribute> attributes);
    }
}
