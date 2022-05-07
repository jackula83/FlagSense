namespace FlagService.Domain.Aggregates.Rules
{
    public interface IUserEvaluator
    {
        bool EvalulateUserFlags(User user);
    }
}
