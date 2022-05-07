using FlagService.Core.Models;
using Framework2.Infra.Data.Entity;

namespace FlagService.Infra.Data.Interfaces
{
    public interface IRuleGroup : IAggregateRoot
    {
        #region EF Relationships
        public int? SegmentId { get; set; }
        public ISegment? Segment { get; set; }

        public int? FlagId { get; set; }
        public IFlag? Flag { get; set; }
        #endregion

        public List<IRule> Rules { get; set; }

        public ServeValue ServeValue { get; set; }

        public bool EvalulateUserFlags(IUser user);
    }
}
