using FlagSense.FlagService.Core.Extensions;
using FlagSense.FlagService.Core.Models;
using FlagSense.FlagService.Domain.Entities;
using FlagSense.FlagService.Domain.Interfaces;
using System.Data;

namespace FlagSense.FlagService.Domain.Data
{
    public class AuditOperations
    {
        public const string AuditSchema = "audit";

        private readonly IRawSqlOperations _rawSqlOperations;

        public AuditOperations(IRawSqlOperations rawSqlOperations)
        {
            _rawSqlOperations = rawSqlOperations;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="before"></param>
        /// <param name="after"></param>
        /// <returns></returns>
        /// <remarks>Audit is perhaps better as its own microservice, however the intention is for the open source
        /// edition to be self-contained, and commercialised version to use an AuditService to keep track of entity
        /// history across all microservices in the domain, it would also require setting up a message queue</remarks>
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

            await _rawSqlOperations.ExecuteRawInsert<TEntity>(AuditSchema, values);
        }

        /// <summary>
        /// Get the latest audit entry
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Audit?> GetAuditEntry<TEntity>(int id)
            where TEntity : FsEntity
        {
            var whereValues = new Dictionary<string, object>()
            {
                {nameof(Audit.RefId), id}
            };
            var recentRecord = (await _rawSqlOperations.ExecuteRawSelect<TEntity>(AuditSchema, whereValues, true)).FirstOrDefault();
            if (recentRecord == default)
                return default;

            return this.ReadAudit(recentRecord);
        }

        public async Task<List<Audit>> EnumerateAuditEntry<TEntity>(int id)
            where TEntity : FsEntity
        {
            var whereValues = new Dictionary<string, object>()
            {
                {nameof(Audit.RefId), id}
            };
            var records = (await _rawSqlOperations.ExecuteRawSelect<TEntity>(AuditSchema, whereValues, false));
            var results = new List<Audit>();

            records.ForEach(x => results.Add(this.ReadAudit(x)));
            return results;
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
