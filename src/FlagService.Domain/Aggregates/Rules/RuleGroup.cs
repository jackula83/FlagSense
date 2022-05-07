using FlagService.Core.Extensions;
using FlagService.Core.Models;
using FlagService.Domain.Aggregates.Rules;
using FlagService.Domain.Models;
using Framework2.Infra.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace FlagService.Domain.Aggregates
{
    public class RuleGroup : FsDataObject, IUserEvaluator, IAggregateRoot
    {
        #region EF Relationships
        public int? SegmentId { get; set; }
        public Segment? Segment { get; set; }

        public int? FlagId { get; set; }
        public Flag? Flag { get; set; }
        #endregion

        public List<Rule> Rules { get; set; } = new();

        public ServeValue ServeValue { get; set; } = new();

        public bool EvalulateUserFlags(User user)
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
                .WithMany(f => f.RuleGroups)
                .HasForeignKey(nameof(FlagId));
            entity
                .HasOne(e => e.Segment)
                .WithMany(s => s.RuleGroups)
                .HasForeignKey(nameof(SegmentId));
        }
    }
}
