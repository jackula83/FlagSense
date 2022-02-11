using FlagSense.FlagService.Domain.Interfaces;
using FlagSense.FlagService.Domain.Models.Abstracts;

namespace FlagSense.FlagService.Domain.Models
{
    public class RuleGroup : FsEntity, IUserEvaluator
    {
        public List<Rule> FlagRules { get; set; } = new();

        public FlagValue ServeValue { get; set; } = new();

        public bool Eval(User user)
            => this.FlagRules.All(x => x.Eval(user));
    }
}
