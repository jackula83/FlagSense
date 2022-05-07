using FlagService.Core.Extensions;
using FlagService.Core.Models;
using FlagService.Domain.Aggregates.Rules;
using FlagService.Infra.Data.Abstracts;
using FlagService.Infra.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace FlagService.Domain.Aggregates
{
    public class Flag : FsDataObject, IRuleTarget, IFlag
    {
        #region EF Relationships
        public int? SegmentId { get; set; }
        public ISegment? Segment { get; set; }
        #endregion

        [StringLength(0x200)]
        public string Name { get; set; } = string.Empty;
        [StringLength(0x10000)]
        public string Description { get; set; } = string.Empty;
        [StringLength(0x10000)]
        public string Alias { get; set; } = string.Empty;

        public bool IsEnabled { get; set; } = false;
        public ServeValue DefaultServeValue { get; set; } = new();
        public List<IRuleGroup> RuleGroups { get; set; } = new();

        public override void SetupEntity(ModelBuilder builder)
        {
            var entity = builder.Entity<Flag>();
            entity
                .HasMany(e => e.RuleGroups);
            entity
                .HasOne(e => e.Segment)
                .WithMany(s => s.Flags.Cast<Flag>())
                .HasForeignKey(nameof(SegmentId));
            entity
                .Property(e => e.DefaultServeValue)
                .HasConversion(
                    v => v.Serialise(),
                    v => v.Deserialise<ServeValue>()!);
            entity
                .HasAlternateKey(e => new { e.Name })
                .HasName("idx_Environment_FlagName");
        }
    }
}
