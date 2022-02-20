using Common.Domain.Core.Models;
using System.Data;

namespace FlagSense.FlagService.Domain.Interfaces
{
    public interface IRawSqlOperations
    {
        Task ExecuteRawInsert<TEntity>(string schemaName, Dictionary<string, object> paramValues) 
            where TEntity : FxEntity;
        Task<List<IDataRecord>> ExecuteRawSelect<TEntity>(string schemaName, Dictionary<string, object> whereValues, bool mostRecent) 
            where TEntity : FxEntity;
    }
}
