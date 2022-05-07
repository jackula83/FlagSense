namespace FlagService.Infra.Data.Interfaces
{
    public interface IUserProperty
    {
        #region EF Relationships
        int UserId { get; set; }
        IUser? User { get; set; }
        #endregion

        public string Key { get; set; }
        public string Value { get; set; }
    }
}
