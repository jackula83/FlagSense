using Common.Domain.Core.Extensions;
using FlagSense.FlagService.Core.Extensions;
using FlagSense.FlagService.Domain.Data;
using FlagSense.FlagService.Domain.Entities;
using FlagSense.FlagService.Domain.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Data;
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
                    p.ContainsKey(nameof(Audit.New)) &&
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

        [Fact]
        public async Task GetAuditEntry_WhenMostRecentIsSelected_ReturnsFirstResult()
        {
            // arrange
            var refId = 2;
            var rawReturnValues = CreateRawReturnValues(refId);
            _sqlOperationsMock
                .Setup(x => x.ExecuteRawSelect<Flag>(AuditOperations.AuditSchema, It.IsAny<Dictionary<string, object>>(), It.IsAny<bool>()))
                .ReturnsAsync(rawReturnValues);

            // act
            var result = await _target.GetAuditEntry<Flag>(refId);
            var results = await _target.EnumerateAuditEntry<Flag>(refId);

            // assert
            Assert.NotNull(result);
            Assert.True(results.Count > 0);
            Assert.True(results.TrueForAll(x => x.RefId == refId));
            Assert.Equal(results[0].Serialise(), result!.Serialise());
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

        private static List<IDataRecord> CreateRawReturnValues(int refId)
        {
            var audit1 = new Audit()
            {
                Id = 1,
                RefId = refId,
                Uuid = Guid.NewGuid(),
                Old = default,
                New = CreateFlagObject().Serialise(),
                CreatedAt = DateTime.UtcNow,
                CreatedBy = String.Empty
            };

            var audit2 = new Audit()
            {
                Id = 2,
                RefId = refId,
                Uuid = Guid.NewGuid(),
                Old = audit1.New,
                New = CreateFlagObject()
                    .Tap(x => x.Name = "aa")
                    .Tap(x => x.Alias = "bb")
                    .Tap(x => x.Description = "cc")
                    .Serialise(),
                CreatedAt = audit1.CreatedAt.AddDays(1),
                CreatedBy = "anonymous"
            };

            var audit3 = new Audit()
            {
                Id = 3,
                RefId = refId + 1,
                Uuid = Guid.NewGuid(),
                Old = default,
                New = CreateFlagObject()
                    .Tap(x => x.Name = "aaa")
                    .Tap(x => x.Alias = "bbb")
                    .Tap(x => x.Description = "ccc")
                    .Serialise(),
                CreatedAt = audit2.CreatedAt.AddMonths(1),
                CreatedBy = "anonymous"
            };

            var record1 = CreateRecordFromAudit(audit1);
            var record2 = CreateRecordFromAudit(audit2);

            return new() { record2, record1 };
        }

        private static IDataRecord CreateRecordFromAudit(Audit audit)
        {
            var record = new Mock<IDataRecord>();

            record.Setup(x => x[nameof(audit.Id)]).Returns(audit.Id);
            record.Setup(x => x[nameof(audit.RefId)]).Returns(audit.RefId);
            record.Setup(x => x[nameof(audit.Uuid)]).Returns(audit.Uuid);
            record.Setup(x => x[nameof(audit.Old)]).Returns(audit?.Old!);
            record.Setup(x => x[nameof(audit.New)]).Returns(audit?.New!);
            record.Setup(x => x[nameof(audit.CreatedAt)]).Returns(audit?.CreatedAt!);
            record.Setup(x => x[nameof(audit.CreatedBy)]).Returns(audit?.CreatedBy!);

            return record.Object;
        }
    }
}
