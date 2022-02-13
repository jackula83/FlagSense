using FlagSense.FlagService.Core.Models;
using FlagSense.FlagService.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace FlagSense.FlagService.Domain.Entities
{
    public enum FlagRuleType : int
    {
        INVALID,
        ONE_OF,
        STARTS_WITH,
        REGEX
    }

    public class Rule : FsEntity, IUserEvaluator
    {
        public int RuleGroupId { get; set; }
        public RuleGroup? RuleGroup { get; set; }
        public string Key { get; set; } = string.Empty;
        public List<Condition> Conditions { get; set; } = new();
        public FlagRuleType RuleType { get; set; } = FlagRuleType.INVALID;

        public bool Eval(User user)
        {
            if (string.IsNullOrWhiteSpace(this.Key))
                return false;

            var userProperty = user.Properties.FirstOrDefault(x => string.Compare(x.Key, this.Key, true) == 0);
            if (userProperty == default)
                return false;

            
            return this.RuleType switch
            {
                FlagRuleType.ONE_OF => this.Conditions.Any(x => string.Compare(x.Value, userProperty.Value, true) == 0),
                FlagRuleType.STARTS_WITH => this.Conditions.Any(x => userProperty.Value.StartsWith(x.Value)),
                FlagRuleType.REGEX => this.Conditions.Any(x => IsRegexValid(x.Value) && Regex.IsMatch(userProperty.Value, x.Value, RegexOptions.IgnoreCase)),
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
                .WithMany(g => g.FlagRules)
                .HasForeignKey(nameof(RuleGroupId));
        }
    }
}
