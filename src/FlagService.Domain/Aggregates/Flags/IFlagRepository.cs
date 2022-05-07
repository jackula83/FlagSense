using Framework2.Infra.Data.Repository;

namespace FlagService.Domain.Aggregates.Flags
{
    public interface IFlagRepository : IEntityRepository<Flag>
    {
    }
}
