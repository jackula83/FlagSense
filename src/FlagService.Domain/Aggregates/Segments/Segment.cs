using FlagService.Core.Extensions;
using FlagService.Core.Models;
using FlagService.Domain.Aggregates.Rules;
using FlagService.Domain.Interfaces;
using FlagService.Domain.Models;
using Framework2.Infra.Data.Entity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace FlagService.Domain.Aggregates
{
    public class Segment : FsDataObject, IRuleTarget, IColourCoding, IAggregateRoot
    {
        #region EF Relationships
        public int EnvironmentId { get; set; }
        public Env? Environment { get; set; }
        #endregion

        [StringLength(0x200)]
        public string Name { get; set; } = string.Empty;
        [StringLength(0x10000)]
        public string Description { get; set; } = string.Empty;

        public static int DefaultColour = Color.DeepSkyBlue.ToArgb();
        public int ColourCoding { get; set; } = DefaultColour;

        public List<Flag> Flags { get; set; } = new();
        public bool IsEnabled { get; set; } = false;
        public ServeValue DefaultServeValue { get; set; } = new();
        public List<RuleGroup> RuleGroups { get; set; } = new();

        public override void SetupEntity(ModelBuilder builder)
        {
            var entity = builder.Entity<Segment>();
            entity.HasMany(e => e.Flags);
            entity.HasMany(e => e.RuleGroups);
            entity
                .HasOne(e => e.Environment)
                .WithMany(v => v.Segments)
                .HasForeignKey(nameof(EnvironmentId));
            entity
                .Property(e => e.DefaultServeValue)
                .HasConversion(
                    v => v.Serialise(),
                    v => v.Deserialise<ServeValue>()!);
        }
    }
}
