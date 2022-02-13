using FlagSense.FlagService.Core.Extensions;
using FlagSense.FlagService.Core.Models;
using FlagSense.FlagService.Domain.Interfaces;
using FlagSense.FlagService.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace FlagSense.FlagService.Domain.Entities
{
    public class Flag : FsEntity, IRuleTarget
    {
        public int EnvironmentId { get; set; }
        public Env? Environment { get; set; }

        public int? SegmentId { get; set; }
        public Segment? Segment { get; set; }

        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Alias { get; set; } = string.Empty;

        public bool IsEnabled { get; set; } = false;
        public FlagValue DefaultServe { get; set; } = new();
        public List<RuleGroup> RuleGroups { get; set; } = new();

        public override void SetupEntity(ModelBuilder builder)
        {
            var entity = builder.Entity<Flag>();
            entity
                .HasMany(e => e.RuleGroups);
            entity
                .HasOne(e => e.Segment)
                .WithMany(s => s.Flags)
                .HasForeignKey(nameof(SegmentId));
            entity
                .HasOne(e => e.Environment)
                .WithMany(v => v.Flags)
                .HasForeignKey(nameof(EnvironmentId));
            entity
                .Property(e => e.DefaultServe)
                .HasConversion(
                    v => v.Serialise(),
                    v => v.Deserialise<FlagValue>()!);
            entity
                .HasAlternateKey(e => new { e.EnvironmentId, e.Name })
                .HasName("idx_Environment_FlagName");
        }
    }
}
