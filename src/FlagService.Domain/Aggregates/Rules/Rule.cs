using FlagService.Infra.Data.Abstracts;
using FlagService.Infra.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace FlagService.Domain.Aggregates.Rules
{
    public class Rule : FsDataObject, IUserEvaluator, IRule
    {
        #region EF Relationships
        public int RuleGroupId { get; set; }
        public IRuleGroup? RuleGroup { get; set; }
        #endregion

        [StringLength(0x200)]
        public string Key { get; set; } = string.Empty;
        public List<ICondition> Conditions { get; set; } = new();
        public FlagRuleType RuleType { get; set; } = FlagRuleType.INVALID;

        public bool EvalulateUserFlags(IUser user)
        {
            if (string.IsNullOrWhiteSpace(Key))
                return false;

            var userProperty = user.Properties.FirstOrDefault(x => string.Compare(x.Key, Key, true) == 0);
            if (userProperty == default)
                return false;

            return RuleType switch
            {
                FlagRuleType.ONE_OF => Conditions.Any(x => string.Compare(x.Value, userProperty.Value, true) == 0),
                FlagRuleType.STARTS_WITH => Conditions.Any(x => userProperty.Value.StartsWith(x.Value)),
                FlagRuleType.REGEX => Conditions.Any(x => IsRegexValid(x.Value) && Regex.IsMatch(userProperty.Value, x.Value, RegexOptions.IgnoreCase)),
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
                .HasOne(x => x.RuleGroup)
                .WithMany(g => g.Rules.Cast<Rule>())
                .HasForeignKey(nameof(RuleGroupId));
        }
    }
}
