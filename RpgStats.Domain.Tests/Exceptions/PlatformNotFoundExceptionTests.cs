using RpgStats.Domain.Exceptions;

namespace RpgStats.Domain.Tests.Exceptions
{
    public class PlatformNotFoundExceptionTests
    {
        [Fact]
        public void PlatformNotFoundException_Creation_Success()
        {
            long platformId = 456;

            var exception = new PlatformNotFoundException(platformId);

            Assert.NotNull(exception);
        }
        
        [Fact]
        public void PlatformNotFoundException_Type_IsCorrect()
        {
            long platformId = 456;

            var exception = new PlatformNotFoundException(platformId);

            Assert.IsType<PlatformNotFoundException>(exception);
        }

        [Fact]
        public void PlatformNotFoundException_Message_IsCorrect()
        {
            long platformId = 456;
            string expectedMessage = $"The Platform with the identifier {platformId} was not found.";

            var exception = new PlatformNotFoundException(platformId);

            Assert.Equal(expectedMessage, exception.Message);
        }
    }
}
