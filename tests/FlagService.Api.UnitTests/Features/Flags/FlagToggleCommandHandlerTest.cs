using FlagService.Domain.Aggregates;
using FlagService.Domain.Aggregates.Flags;
using FlagService.Domain.Auditing;
using FlagService.Domain.Contexts;
using FlagService.Domain.Features.Flags.Commands;
using FlagService.Infra.Data.Repositories;
using Framework2.Infra.Data.UnitTests.Utilities;
using Framework2.Infra.MQ.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace FlagService.Api.UnitTests.Features.Flags
{
    public class FlagToggleCommandHandlerTest
    {
        private readonly FlagToggleCommandHandler _sut;
        private readonly FsSqlServerContext _context;
        private readonly AuditOperations _auditOperations;
        private readonly IFlagRepository _flagRepository;
        private readonly Mock<IEventQueue> _eventQueueMock;
        private readonly NullLogger<FlagToggleCommandHandler> _logger;

        public FlagToggleCommandHandlerTest()
        {
            _eventQueueMock = new();
            _logger = NullLogger<FlagToggleCommandHandler>.Instance;
            _context = Utils.CreateInMemoryDatabase<FsSqlServerContext>(nameof(FlagToggleCommandHandlerTest))!;
            _auditOperations = new(_eventQueueMock.Object, _context);
            _flagRepository = new FlagRepository(_context, _auditOperations);
            _sut = new(_flagRepository, _logger);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task Handle_GivenFlag_ShouldReturnFlagWithNegatedIsEnabled(bool isEnabled)
        {
            // arrange
            var flag = new Flag
            {
                Id = 1,
                IsEnabled = isEnabled
            };
            await _flagRepository.Add(flag);
            await _flagRepository.Save();

            // act
            var response = await _sut.Handle(new() { FlagId = flag.Id });

            // assert
            Assert.NotNull(response?.Flag);
            Assert.Equal(response!.Flag!.IsEnabled, !isEnabled);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task Handle_GivenFlag_ResultShouldBeSavedInRepository(bool isEnabled)
        {
            // arrange
            var flag = new Flag
            {
                Id = 1,
                IsEnabled = isEnabled
            };
            await _flagRepository.Add(flag);
            await _flagRepository.Save();

            // act
            await _sut.Handle(new() { FlagId = flag.Id });
            _context.ChangeTracker.Clear();
            var flagFromRepository = await _flagRepository.Get(flag.Id);

            // assert
            Assert.NotNull(flagFromRepository);
            Assert.Equal(flagFromRepository!.IsEnabled, !isEnabled);
        }

        [Fact]
        public async Task Handle_GivenFlagDoesntExist_ResultReturnNullFlag()
        {
            // act
            var response = await _sut.Handle(new() { FlagId = 1 });

            // assert
            Assert.NotNull(response);
            Assert.Null(response!.Flag);
        }
    }
}
