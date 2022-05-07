using FlagService.Domain.Aggregates;
using Xunit;

namespace FlagSense.FlagService.UnitTests.Domain.Models
{
    public class UserTest
    {
        [Fact]
        public void IsAnonymous_AfterInit_ReturnsTrue()
        {
            // arrange
            var user = new User();

            // assert
            Assert.True(user.IsAnonymous);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void IsAnonymous_SetToProvidedValue_IsProvidedValue(bool isAnonymous)
        {
            // arrange
            var user = new User
            {

                // act
                IsAnonymous = isAnonymous
            };

            // assert
            Assert.Equal(isAnonymous, user.IsAnonymous);
        }
    }
}
