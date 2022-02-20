using Common.Domain.Core.Models;
using FlagSense.FlagService.Domain.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace FlagSense.FlagService.Domain.Data
{
    public class RawSqlOperations : IRawSqlOperations
    {
        private readonly FsContext _context;

        public RawSqlOperations(FsContext context)
        {
            _context = context;
        }

        /// <summary>
        /// TODO: Move this to Common 
        /// </summary>
        /// <remarks>Warning, possible SQL injection, ensure provided values are safe; only testable with integration test</remarks>
        public async Task ExecuteRawInsert<TEntity>(string schemaName, Dictionary<string, object> paramValues)
            where TEntity : FxEntity
        {
            var tableName = _context.Model.FindEntityType(typeof(TEntity))!.GetTableName()!;

            var table = $"{schemaName}.{tableName} ";
            var param = $"{string.Join(",", paramValues.Keys)}";
            var binding = $"{string.Join(",", paramValues.Keys.Select(x => $"@{x}_param"))}";

            var statement = $"INSERT INTO {table} ({param}) VALUES ({binding})";
            var sqlParameters = paramValues.Keys.Select(x => (IDbDataParameter)new SqlParameter($"@{x}_param", paramValues[x]));

            await _context.Database.ExecuteSqlRawAsync(statement, sqlParameters.ToArray());
        }

        /// <summary>
        /// TODO: Move this to Common 
        /// </summary>
        /// <remarks>Warning, possible SQL injection, ensure provided values are safe; only testable with integration test</remarks>
        public async Task<List<IDataRecord>> ExecuteRawSelect<TEntity>(string schemaName, Dictionary<string, object> whereValues, bool mostRecent)
            where TEntity: FxEntity
        {
            var tableName = _context.Model.FindEntityType(typeof(TEntity))!.GetTableName()!;

            var main = mostRecent ? $"TOP 1" : string.Empty;
            var table = $"* FROM {schemaName}.{tableName} ";
            var where = string.Empty;
            var sqlParameters = Enumerable.Empty<IDbDataParameter>();
            if (whereValues != null)
            {
                where = string.Join("AND", whereValues.Keys.Select(x => $"{x} = @{x}_param"));
                sqlParameters = whereValues.Keys.Select(x => (IDbDataParameter)new SqlParameter($"@{x}_param", whereValues[x]));
            }
            var statement = $"SELECT {main} FROM {table}";
            if (!string.IsNullOrEmpty(where))
                statement += $" WHERE {where}";
            if (mostRecent)
                statement += $" ORDER BY CreatedAt DESC";

            var results = new List<IDataRecord>();
            using var command = _context.Database.GetDbConnection().CreateCommand();
            command.CommandText = statement;
            command.Parameters.AddRange(sqlParameters.ToArray());
            _context.Database.OpenConnection();

            var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
                results.Add(reader);

            return results;
        }
    }
}
