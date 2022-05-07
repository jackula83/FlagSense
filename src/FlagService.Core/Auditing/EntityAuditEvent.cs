using Framework2.Infra.Data.Entity;
using Framework2.Infra.MQ.Core;

namespace FlagService.Core.Auditing
{
    /// <summary>
    /// Move this event to Observability service shared lib
    /// </summary>
    /// <typeparam name="TAggregateRoot"></typeparam>
    public class EntityAuditEvent<TAggregateRoot> : FxEvent
        where TAggregateRoot : IAggregateRoot
    {
        public string? SchemaName { get; set; }
        public string TableName { get; set; }
        public TAggregateRoot? OldAggregate { get; set; }
        public TAggregateRoot? NewAggregate { get; set; }

        public EntityAuditEvent(string tableName, string? schemaName)
        {
            TableName = tableName;
            SchemaName = schemaName;
        }
    }
}
