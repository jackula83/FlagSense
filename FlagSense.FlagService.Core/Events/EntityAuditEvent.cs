using Common.Domain.Core.Models;
using FlagSense.FlagService.Core.Models;

namespace FlagSense.FlagService.Core.Events
{
    /// <summary>
    /// Move this event to Observability service shared lib
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class EntityAuditEvent<TEntity> : FxEvent
        where TEntity : FsEntity
    {
        public string? SchemaName { get; set; }
        public string TableName { get; set; }
        public TEntity? OldEntity { get; set; }
        public TEntity? NewEntity { get; set; }

        public EntityAuditEvent(string tableName, string? schemaName)
        {
            this.TableName = tableName;
            this.SchemaName = schemaName;
        }
    }
}
