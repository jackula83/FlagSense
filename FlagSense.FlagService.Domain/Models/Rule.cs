using FlagSense.FlagService.Domain.Interfaces;
using FlagSense.FlagService.Domain.Models.Abstracts;
using System.Text.RegularExpressions;

namespace FlagSense.FlagService.Domain.Models
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
        public string Key { get; set; } = string.Empty;
        public List<string> Conditions { get; set; } = new();
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
                FlagRuleType.ONE_OF => this.Conditions.Any(x => string.Compare(x, userProperty.Value, true) == 0),
                FlagRuleType.STARTS_WITH => this.Conditions.Any(x => userProperty.Value.StartsWith(x)),
                FlagRuleType.REGEX => this.Conditions.Any(x => IsRegexValid(x) && Regex.IsMatch(userProperty.Value, x, RegexOptions.IgnoreCase)),
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
    }
}
