using Common.Domain.Core.Data;
using FlagSense.FlagService.Core.Extensions;
using FlagSense.FlagService.Core.Models;
using FlagSense.FlagService.Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace FlagSense.FlagService.Domain.Data
{
    /// <summary>
    /// Context for SqlServer, use Context Factory to support different database providers
    /// </summary>
    public class FsContext : FxSqlServerDbContext
    {
        public const string AuditSchema = "audit";

        public DbSet<Env> Environments { get; set; }
        public DbSet<Segment> Segments { get; set; }
        public DbSet<Flag> Flags { get; set; }
        public DbSet<Rule> Rules { get; set; }
        public DbSet<RuleGroup> RuleGroups { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserProperty> UserProperties { get; set; }

#pragma warning disable 8618
        public FsContext(DbContextOptions options) : base(options)
        {
            this.SavedChanges += FsContext_SavedChanges;
        }
#pragma warning restore 8618

        protected override void Setup<TEntityType>(ModelBuilder builder)
        {
            base.Setup<TEntityType>(builder);

            var model = Activator.CreateInstance<TEntityType>() as FsEntity;
            model!.Setup(builder);
        }

        private void FsContext_SavedChanges(object? sender, SavedChangesEventArgs e)
        {
            throw new NotImplementedException();
        }

        public async Task AddAuditEntry<TEntity>(TEntity? before, TEntity after)
            where TEntity : FsEntity
        {
            var tableName = this.Model.FindEntityType(typeof(TEntity))!.GetTableName()!;

            var values = new Dictionary<string, object>()
            {
                {"Uuid", Guid.NewGuid().ToString()},
                {"New", after.Serialise()},
                {"CreatedAt", DateTime.UtcNow},
                {"CreatedBy", after.CreatedBy! }
            };

            if (before != default)
                values["Old"] = before.Serialise();

            await this.ExecuteRawInsert(tableName, AuditSchema, values);
        }

        /// <summary>
        /// TODO: Move this to Common 
        /// </summary>
        /// <remarks>Warning, possible SQL injection, ensure provided values are safe</remarks>
        protected async Task ExecuteRawInsert(string tableName, string schemaName, Dictionary<string, object> values)
        {
            var main = $"INSERT INTO {schemaName}.{tableName} ";
            var param = $"({string.Join(",", values.Keys)}";
            var binding = $"{string.Join(",", values.Keys.Select(x => $"@{x}"))}";

            var statement = $"{main} {param} VALUES {binding}";
            var sqlParameters = values.Keys.Select(x => new SqlParameter($"@{x}", values[x]));

            await this.Database.ExecuteSqlRawAsync(statement, sqlParameters.ToArray());
        }
    }
}