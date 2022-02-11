using FlagSense.FlagService.Domain.Models;

namespace FlagSense.FlagService.Domain.Interfaces
{
    public interface IUserEvaluator
    {
        bool Eval(User user);
    }
}
