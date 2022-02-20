using Common.Domain.Core.Extensions;
using FlagSense.FlagService.Core.Extensions;
using FlagSense.FlagService.Domain.Data;
using FlagSense.FlagService.Domain.Entities;
using FlagSense.FlagService.Domain.Interfaces;
using Moq;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Xunit;

namespace FlagSense.FlagService.UnitTests.Domain.Data
{
    public class AuditOperationsTest
    {
        private readonly Mock<IRawSqlOperations> _sqlOperationsMock;
        private readonly AuditOperations _target;

        public AuditOperationsTest()
        {
            _sqlOperationsMock = new();
            _target = new(_sqlOperationsMock.Object);
        }

        [Theory]
        [MemberData(nameof(ProvideBeforeAfterFlagData))]
        public async Task AddAuditEntry_AddNewAuditEntry_RawSqlPassedCorrectSchemaAndValues([AllowNull] Flag before, Flag after)
        {
            // arrange
            var beforeSerial = before?.Serialise();
            var afterSerial = after.Serialise();

            // act
            await _target.AddAuditEntry(before, after);

            // assert
            // TODO: need to refactor this, test in current state is *not* readable
            _sqlOperationsMock.Verify(x => x.ExecuteRawInsert<Flag>(
                AuditOperations.AuditSchema,
                It.Is<Dictionary<string, object>>(p => 
                    (int)p[nameof(Audit.RefId)] == after.Id && 
                    string.Compare(p[nameof(Audit.New)].ToString(), afterSerial, true) == 0)
                ));
            _sqlOperationsMock.Verify(x => x.ExecuteRawInsert<Flag>(
                AuditOperations.AuditSchema,
                It.Is<Dictionary<string, object>>(p =>
                    before == default && !p.ContainsKey(nameof(Audit.Old)) ||
                    (int)p[nameof(Audit.RefId)] == before!.Id &&
                    string.Compare(p[nameof(Audit.Old)].ToString(), beforeSerial, true) == 0)
                ));
        }

        public static IEnumerable<object?[]> ProvideBeforeAfterFlagData()
        {
            yield return new object?[]
            {
                null,
                CreateFlagObject()
            };
            yield return new object[]
            {
                CreateFlagObject(),
                // just to make "after" a bit different
                CreateFlagObject()
                    .Tap(x => x.Name = "aa")
                    .Tap(x => x.Alias = "bb")
                    .Tap(x => x.Description = "cc")
            };
        }

        private static Flag CreateFlagObject()
            => new Flag()
            {
                Id = 1,
                Name = "a",
                Alias = "b",
                Description = "c",
                DefaultServe = new() { State = false },
                IsEnabled = false,
                RuleGroups = new()
            };
    }
}
