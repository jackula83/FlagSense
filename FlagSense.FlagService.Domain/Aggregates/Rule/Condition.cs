using Common.Domain.Core.Extensions;
using FlagService.Infra.Data.Abstracts;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace FlagSense.FlagService.Domain.Entities
{
    public class Condition : FsDataObject
    {
        public int RuleId { get; set; }
        public Rule? Rule { get; set; }

        [Required]
        [StringLength(0x10000)]
        public string Value { get; set; } = string.Empty;

        public static implicit operator Condition(string value)
            => new Condition().Tap(x => x.Value = value);

        public override void SetupEntity(ModelBuilder builder)
        {
            var entity = builder.Entity<Condition>();
            entity
                .HasOne(x => x.Rule)
                .WithMany(r => r.Conditions)
                .HasForeignKey(nameof(RuleId));
        }
    }
}
