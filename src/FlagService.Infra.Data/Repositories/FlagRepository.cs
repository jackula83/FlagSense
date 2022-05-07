using FlagService.Core.Auditing;
using FlagService.Infra.Data.Abstracts;
using FlagService.Infra.Data.Interfaces;

namespace FlagService.Infra.Data.Repositories
{
    public class FlagRepository : FsRepository<IFlag>, IFlagRepository
    {
        public FlagRepository(FsSqlServerContext context, AuditOperations auditOperations) : base(context, auditOperations)
        {
        }
    }
}
