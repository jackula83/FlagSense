using FlagSense.FlagService.Core.Extensions;
using FlagSense.FlagService.Core.Models;
using FlagSense.FlagService.Domain.Entities;
using FlagSense.FlagService.Domain.Interfaces;
using System.Data;

namespace FlagSense.FlagService.Domain.Data
{
    /// <summary>
    /// Audit is perhaps better as its own microservice, however the intention is for the open source
    /// edition to be self-contained, and commercialised version to use an AuditService to keep track of entity
    /// history across all microservices in the domain, it would also require setting up a message queue.
    /// To move this function to the message queue all it needs is a new interface added, although the tests
    /// will need to change
    /// </summary>
    public class AuditOperations
    {
        public const string AuditSchema = "audit";

        private readonly IRawSqlOperations _rawSqlOperations;

        public AuditOperations(IRawSqlOperations rawSqlOperations)
        {
            _rawSqlOperations = rawSqlOperations;
        }

        public async Task AddAuditEntry<TEntity>(TEntity? before, TEntity after)
            where TEntity : FsEntity
        {
            var values = new Dictionary<string, object>()
            {
                {nameof(Audit.Uuid), Guid.NewGuid().ToString()},
                {nameof(Audit.RefId), after.Id },
                {nameof(Audit.New), after.Serialise()},
                {nameof(Audit.CreatedAt), DateTime.UtcNow},
                {nameof(Audit.CreatedBy), after.CreatedBy! }
            };

            if (before != default)
                values[nameof(Audit.Old)] = before.Serialise();
        }

        private Audit ReadAudit(IDataRecord record)
            => new()
            {
                Id = (int)record[nameof(Audit.Id)],
                Uuid = (Guid)record[nameof(Audit.Uuid)],
                RefId = (int)record[nameof(Audit.RefId)],
                Old = record[nameof(Audit.Old)]?.ToString(),
                New = record[nameof(Audit.New)].ToString()!,
                CreatedAt = (DateTime)record[nameof(Audit.CreatedAt)],
                CreatedBy = record[nameof(Audit.CreatedBy)]?.ToString()
            };
    }
}
