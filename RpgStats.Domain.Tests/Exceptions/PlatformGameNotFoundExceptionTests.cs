using RpgStats.Domain.Exceptions;

namespace RpgStats.Domain.Tests.Exceptions
{
    public class PlatformGameNotFoundExceptionTests
    {
        [Fact]
        public void PlatformGameNotFoundException_Creation_Success()
        {
            // Arrange
            long platformGameId = 456;

            // Act
            var exception = new PlatformGameNotFoundException(platformGameId);

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<PlatformGameNotFoundException>(exception);
        }

        [Fact]
        public void PlatformGameNotFoundException_Message_CorrectFormat()
        {
            // Arrange
            long platformGameId = 456;
            string expectedMessage = $"The PlatformGame with the identifier {platformGameId} was not found.";

            // Act
            var exception = new PlatformGameNotFoundException(platformGameId);

            // Assert
            Assert.Equal(expectedMessage, exception.Message);
        }
    }
}
