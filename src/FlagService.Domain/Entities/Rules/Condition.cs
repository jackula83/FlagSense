using FlagService.Core.Models;
using Framework2.Core.Extensions;
using Framework2.Infra.Data.Entity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace FlagService.Domain.Entities.Rules
{
    public enum ConditionOperator : int
    {
        INVALID = 0,
        ONE_OF,
        STARTS_WITH,
        REGEX
    }

    public class Condition : FsDataObject, IEntity
    {
        #region EF Relationships
        public int RuleId { get; set; }
        public Rule? Rule { get; set; }
        #endregion

        [Required]
        [StringLength(0x200)]
        public string AttributeName { get; set; } = string.Empty;

        [Required]
        public ConditionOperator Operator { get; set; }

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
