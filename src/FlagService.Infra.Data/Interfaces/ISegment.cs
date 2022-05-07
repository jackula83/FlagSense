using FlagService.Core.Models;
using Framework2.Infra.Data.Entity;

namespace FlagService.Infra.Data.Interfaces
{
    public interface ISegment : IAggregateRoot
    {
        #region EF Relationships
        int EnvironmentId { get; set; }
        IEnv? Environment { get; set; }
        #endregion

        string Name { get; set; }
        string Description { get; set; }

        int ColourCoding { get; set; }

        List<IFlag> Flags { get; set; }
        bool IsEnabled { get; set; }
        ServeValue DefaultServeValue { get; set; }
        List<IRuleGroup> RuleGroups { get; set; }
    }
}
