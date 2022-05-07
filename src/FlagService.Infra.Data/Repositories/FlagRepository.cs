using FlagService.Domain.Aggregates;
using FlagService.Domain.Aggregates.Flags;
using FlagService.Domain.Auditing;
using FlagService.Domain.Contexts;
using FlagService.Infra.Data.Abstracts;

namespace FlagService.Infra.Data.Repositories
{
    public sealed class FlagRepository : FsRepository<Flag>, IFlagRepository
    {
        public FlagRepository(FsSqlServerContext context, AuditOperations auditOperations) : base(context, auditOperations)
        {
        }
    }
}
