using FlagService.Core.Models;
using FlagService.Infra.Data.Interfaces;

namespace FlagService.Domain.Aggregates.Rules
{
    public interface IRuleTarget
    {
        /// <summary>
        /// Whether the flag is ON or OFF, by default flag value always returns false when flag is disabled
        /// </summary>
        bool IsEnabled { get; set; }

        /// <summary>
        /// The default serve value when flag is ON and evaluation does not match any rules
        /// </summary>
        ServeValue DefaultServeValue { get; set; }

        /// <summary>
        /// List of rules associated with this flag
        /// </summary>
        List<IRuleGroup> RuleGroups { get; set; }
    }
}
