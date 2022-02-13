using FlagSense.FlagService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlagSense.FlagService.Domain.Interfaces
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
        FlagValue DefaultServe { get; set; }

        /// <summary>
        /// List of rules associated with this flag
        /// </summary>
        List<RuleGroup> RuleGroups { get; set; }
    }
}
