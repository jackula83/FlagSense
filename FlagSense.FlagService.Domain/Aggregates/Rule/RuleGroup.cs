using FlagSense.FlagService.Core.Extensions;
using FlagSense.FlagService.Domain.Interfaces;
using FlagSense.FlagService.Domain.Models;
using FlagService.Infra.Data.Abstracts;
using Microsoft.EntityFrameworkCore;

namespace FlagSense.FlagService.Domain.Entities
{
    public class RuleGroup : FsDataObject, IUserEvaluator
    {
        public int? SegmentId { get; set; }
        public Segment? Segment { get; set; }

        public int? FlagId { get; set; }
        public Flag? Flag { get; set; }

        public List<Rule> FlagRules { get; set; } = new();

        public FlagValue ServeValue { get; set; } = new();

        public bool Eval(User user)
            => this.FlagRules.All(x => x.Eval(user));

        public override void SetupEntity(ModelBuilder builder)
        {
            var entity = builder.Entity<RuleGroup>();
            entity
                .Property(e => e.ServeValue)
                .HasConversion(
                    v => v.Serialise(),
                    v => v.Deserialise<FlagValue>()!);
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
