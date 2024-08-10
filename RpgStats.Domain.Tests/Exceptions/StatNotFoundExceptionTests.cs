using RpgStats.Domain.Exceptions;
using Xunit;

namespace RpgStats.Domain.Tests.Exceptions
{
    public class StatNotFoundExceptionTests
    {
        [Fact]
        public void StatNotFoundException_Creation_Success()
        {
            // Arrange
            long statId = 456;

            // Act
            var exception = new StatNotFoundException(statId);

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<StatNotFoundException>(exception);
        }

        [Fact]
        public void StatNotFoundException_Message_CorrectFormat()
        {
            // Arrange
            long statId = 456;
            string expectedMessage = $"The Stat with the identifier {statId} was not found.";

            // Act
            var exception = new StatNotFoundException(statId);

            // Assert
            Assert.Equal(expectedMessage, exception.Message);
        }
    }
}
