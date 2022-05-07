using Framework2.Infra.Data.Entity;

namespace FlagService.Infra.Data.Interfaces
{
    public interface IUser : IAggregateRoot
    {
        List<IUserProperty> Properties { get; set; }
    }
}
