namespace FlagService.Infra.Data.Interfaces
{
    public enum FlagRuleType : int
    {
        INVALID,
        ONE_OF,
        STARTS_WITH,
        REGEX
    }

    public interface IRule
    {
        #region EF Relationships
        int RuleGroupId { get; set; }
        IRuleGroup? RuleGroup { get; set; }
        #endregion

        string Key { get; set; }
        List<ICondition> Conditions { get; set; }
        FlagRuleType RuleType { get; set; }

        bool EvalulateUserFlags(IUser user);
    }
}
