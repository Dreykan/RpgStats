using RpgStats.Domain.Exceptions;

namespace RpgStats.Domain.Tests.Exceptions
{
    public class StatValueNotFoundExceptionTests
    {
        [Fact]
        public void StatValueNotFoundException_Creation_Success()
        {
            // Arrange
            long statValueId = 456;

            // Act
            var exception = new StatValueNotFoundException(statValueId);

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<StatValueNotFoundException>(exception);
        }

        [Fact]
        public void StatValueNotFoundException_Message_CorrectFormat()
        {
            // Arrange
            long statValueId = 456;
            string expectedMessage = $"The StatValue with the identifier {statValueId} was not found.";

            // Act
            var exception = new StatValueNotFoundException(statValueId);

            // Assert
            Assert.Equal(expectedMessage, exception.Message);
        }
    }
}
