using FlagSense.FlagService.Domain.Entities;

namespace FlagService.Domain.Aggregates.Rules
{
    public interface IUserEvaluator
    {
        bool EvalulateUserFlags(User user);
    }
}
