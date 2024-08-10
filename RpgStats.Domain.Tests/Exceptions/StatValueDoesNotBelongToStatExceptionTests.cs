using RpgStats.Domain.Exceptions;

namespace RpgStats.Domain.Tests.Exceptions
{
    public class StatValueDoesNotBelongToStatExceptionTests
    {
        [Fact]
        public void StatValueDoesNotBelongToStatException_Creation_Success()
        {
            // Arrange
            long statvalueId = 123;
            long statId = 456;

            // Act
            var exception = new StatValueDoesNotBelongToStatException(statvalueId, statId);

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<StatValueDoesNotBelongToStatException>(exception);
        }

        [Fact]
        public void StatValueDoesNotBelongToStatException_Message_CorrectFormat()
        {
            // Arrange
            long statvalueId = 123;
            long statId = 456;
            string expectedMessage = $"The StatValue with the identifier {statvalueId} does not belong to the Stat with the identifier {statId}.";

            // Act
            var exception = new StatValueDoesNotBelongToStatException(statvalueId, statId);

            // Assert
            Assert.Equal(expectedMessage, exception.Message);
        }
    }
}
