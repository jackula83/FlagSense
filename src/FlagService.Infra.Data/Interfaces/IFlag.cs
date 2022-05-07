using FlagService.Core.Models;
using Framework2.Infra.Data.Entity;

namespace FlagService.Infra.Data.Interfaces
{
    public interface IFlag : IAggregateRoot
    {
        #region EF Relationships
        int? SegmentId { get; set; }
        ISegment? Segment { get; set; }
        #endregion

        string Name { get; set; }
        string Description { get; set; }
        string Alias { get; set; }

        bool IsEnabled { get; set; }
        ServeValue DefaultServeValue { get; set; }
        List<IRuleGroup> RuleGroups { get; set; }
    }
}
