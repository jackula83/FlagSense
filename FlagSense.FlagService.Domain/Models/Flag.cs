using FlagSense.FlagService.Domain.Models.Abstracts;

namespace FlagSense.FlagService.Domain.Models
{
    public class Flag : FsEntity
    {
        /// <summary>
        /// Whether the flag is ON or OFF, by default flag value always returns false when flag is disabled
        /// </summary>
        public bool IsEnabled { get; set; } = false;

        /// <summary>
        /// The default serve value when flag is ON and evaluation does not match any rules
        /// </summary>
        public FlagValue DefaultServe { get; set; } = new();

        /// <summary>
        /// List of rules associated with this flag
        /// </summary>
        public List<RuleGroup> RuleGroups { get; set; } = new();
    }
}
