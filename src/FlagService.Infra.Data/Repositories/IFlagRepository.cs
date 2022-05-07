using FlagService.Infra.Data.Interfaces;
using Framework2.Infra.Data.Repository;

namespace FlagService.Infra.Data.Repositories
{
    public interface IFlagRepository : IEntityRepository<IFlag>
    {
    }
}
