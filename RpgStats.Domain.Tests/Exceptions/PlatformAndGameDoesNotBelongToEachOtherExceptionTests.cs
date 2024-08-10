using RpgStats.Domain.Exceptions;

namespace RpgStats.Domain.Tests.Exceptions
{
    public class PlatformAndGameDoesNotBelongToEachOtherExceptionTests
    {
        [Fact]
        public void PlatformAndGameDoesNotBelongToEachOtherException_Creation_Success()
        {
            // Arrange
            long platformId = 1;
            long gameId = 2;

            // Act
            var exception = new PlatformAndGameDoesNotBelongToEachOtherException(platformId, gameId);

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<PlatformAndGameDoesNotBelongToEachOtherException>(exception);
        }

        [Fact]
        public void PlatformAndGameDoesNotBelongToEachOtherException_Message_CorrectFormat()
        {
            // Arrange
            long platformId = 1;
            long gameId = 2;
            string expectedMessage = $"The Game with the identifier {gameId} is in no relationship to the Platform with the identifier {platformId}.";

            // Act
            var exception = new PlatformAndGameDoesNotBelongToEachOtherException(platformId, gameId);

            // Assert
            Assert.Equal(expectedMessage, exception.Message);
        }
    }
}
