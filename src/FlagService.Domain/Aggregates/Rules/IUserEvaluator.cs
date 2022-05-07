using FlagService.Infra.Data.Interfaces;

namespace FlagService.Domain.Aggregates.Rules
{
    public interface IUserEvaluator
    {
        bool EvalulateUserFlags(IUser user);
    }
}
