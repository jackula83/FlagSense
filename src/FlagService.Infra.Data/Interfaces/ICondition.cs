namespace FlagService.Infra.Data.Interfaces
{
    public interface ICondition
    {
        #region EF Relationships
        int RuleId { get; set; }
        IRule? Rule { get; set; }
        #endregion

        string Value { get; set; }
    }
}
