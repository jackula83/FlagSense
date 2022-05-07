using FlagService.Core.Extensions;
using FlagService.Core.Models;
using FlagService.Domain.Aggregates.Rules;
using FlagService.Infra.Data.Abstracts;
using FlagService.Infra.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FlagService.Domain.Aggregates
{
    public class RuleGroup : FsDataObject, IUserEvaluator, IRuleGroup
    {
        #region EF Relationships
        public int? SegmentId { get; set; }
        public ISegment? Segment { get; set; }

        public int? FlagId { get; set; }
        public IFlag? Flag { get; set; }
        #endregion

        public List<IRule> Rules { get; set; } = new();

        public ServeValue ServeValue { get; set; } = new();

        public bool EvalulateUserFlags(IUser user)
            => Rules.All(x => x.EvalulateUserFlags(user));

        public override void SetupEntity(ModelBuilder builder)
        {
            var entity = builder.Entity<RuleGroup>();
            entity
                .Property(e => e.ServeValue)
                .HasConversion(
                    v => v.Serialise(),
                    v => v.Deserialise<ServeValue>()!);
            entity
                .HasOne(e => e.Flag)
                .WithMany(f => f.RuleGroups.Cast<RuleGroup>())
                .HasForeignKey(nameof(FlagId));
            entity
                .HasOne(e => e.Segment)
                .WithMany(s => s.RuleGroups.Cast<RuleGroup>())
                .HasForeignKey(nameof(SegmentId));
        }
    }
}
