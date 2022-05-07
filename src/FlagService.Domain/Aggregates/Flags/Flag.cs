using FlagService.Core.Extensions;
using FlagService.Domain.Aggregates.Rules;
using FlagService.Domain.Models;
using FlagService.Infra.Data.Abstracts;
using Framework2.Infra.Data.Entity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace FlagService.Domain.Aggregates
{
    public class Flag : FsDataObject, IRuleTarget, IAggregateRoot
    {
        #region EF Relationships
        public int? SegmentId { get; set; }
        public Segment? Segment { get; set; }
        #endregion

        [StringLength(0x200)]
        public string Name { get; set; } = string.Empty;
        [StringLength(0x10000)]
        public string Description { get; set; } = string.Empty;
        [StringLength(0x10000)]
        public string Alias { get; set; } = string.Empty;

        public bool IsEnabled { get; set; } = false;
        public ServeValue DefaultServeValue { get; set; } = new();
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
