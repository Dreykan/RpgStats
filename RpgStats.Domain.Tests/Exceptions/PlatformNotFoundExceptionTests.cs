using RpgStats.Domain.Exceptions;

namespace RpgStats.Domain.Tests.Exceptions
{
    public class PlatformNotFoundExceptionTests
    {
        [Fact]
        public void PlatformNotFoundException_Creation_Success()
        {
            // Arrange
            long platformId = 456;

            // Act
            var exception = new PlatformNotFoundException(platformId);

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<PlatformNotFoundException>(exception);
        }

        [Fact]
        public void PlatformNotFoundException_Message_CorrectFormat()
        {
            // Arrange
            long platformId = 456;
            string expectedMessage = $"The Platform with the identifier {platformId} was not found.";

            // Act
            var exception = new PlatformNotFoundException(platformId);

            // Assert
            Assert.Equal(expectedMessage, exception.Message);
        }
    }
}
