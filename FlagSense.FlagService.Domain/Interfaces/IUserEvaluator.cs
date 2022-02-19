using FlagSense.FlagService.Domain.Entities;

namespace FlagSense.FlagService.Domain.Interfaces
{
    public interface IUserEvaluator
    {
        bool Eval(User user);
    }
}
