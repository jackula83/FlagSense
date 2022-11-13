using FlagService.Core.Extensions;
using FlagService.Core.Models;
using FlagService.Domain.Aggregates;
using FlagService.Domain.Aggregates.Users;
using FlagService.Domain.Models;
using Framework2.Infra.Data.Entity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace FlagService.Domain.Entities.Rules
{
    public class Rule : FsDataObject, IEntity, IUserEvaluator
    {
        #region EF Relationships
        public int FlagId { get; set; }
        public Flag? Flag { get; set; }
        #endregion

        [StringLength(0x200)]
        public List<Condition> Conditions { get; set; } = new();

        /// <summary>
        /// If null, then inherits the defualt serve value
        /// </summary>
        public ServeValue? Value { get; set; }

        public bool EvaluateUserAttributes(User user)
        {
            if (Conditions.Count == 0)
                return false;

            var userAttributeKeys = user.Attributes.Select(u => u.Key);
            var conditionKeys = this.Conditions.Select(c => c.AttributeName);
            var conditionsToEvaluate = userAttributeKeys.Intersect(conditionKeys);

            if (conditionsToEvaluate.Count() < conditionKeys.Count())
                return false;

            foreach (var condition in Conditions)
            {
                var attributeValue = user.GetUserAttribute(condition.AttributeName)?.Value;
                if (attributeValue == default)
                    return false;

                if (!EvaluateAttribute(attributeValue, condition))
                    return false;
            }

            return true;
        }

        public bool EvaluateAttributes(List<UserAttribute> attributes)
        {
            return EvaluateUserAttributes(new() { Attributes = attributes });
        }

        private bool EvaluateAttribute(string attributeValue, Condition condition)
        {
            return condition.Operator switch
            {
                ConditionOperator.ONE_OF => condition.Value.Split(",").FirstOrDefault(x => string.Compare(x.Trim(), attributeValue, true) == 0) != default,
                ConditionOperator.STARTS_WITH => attributeValue.StartsWith(condition.Value, StringComparison.InvariantCultureIgnoreCase),
                ConditionOperator.REGEX => IsRegexValid(condition.Value) && Regex.IsMatch(attributeValue, condition.Value, RegexOptions.IgnoreCase),
                _ => false
            };
        }

        private static bool IsRegexValid(string pattern)
        {
            try
            {
                new Regex(pattern);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public override void SetupEntity(ModelBuilder builder)
        {
            var entity = builder.Entity<Rule>();
            entity
                .HasOne(x => x.Flag)
                .WithMany(g => g.Rules)
                .HasForeignKey(nameof(FlagId));
            entity
                .Property(e => e.Value)
                .HasConversion(
                    v => v.Serialise(),
                    v => v.Deserialise<ServeValue>()!);
        }
    }
}
