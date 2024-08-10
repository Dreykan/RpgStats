using RpgStats.Domain.Exceptions;

namespace RpgStats.Domain.Tests.Exceptions
{
    public class StatValueDoesNotBelongToCharacterExceptionTests
    {
        [Fact]
        public void StatValueDoesNotBelongToCharacterException_Creation_Success()
        {
            // Arrange
            long statvalueId = 456;
            long characterId = 789;

            // Act
            var exception = new StatValueDoesNotBelongToCharacterException(statvalueId, characterId);

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<StatValueDoesNotBelongToCharacterException>(exception);
        }

        [Fact]
        public void StatValueDoesNotBelongToCharacterException_Message_CorrectFormat()
        {
            // Arrange
            long statvalueId = 456;
            long characterId = 789;
            string expectedMessage = $"The StatValue with the identifier {statvalueId} does not belong to the Character with the identifier {characterId}.";

            // Act
            var exception = new StatValueDoesNotBelongToCharacterException(statvalueId, characterId);

            // Assert
            Assert.Equal(expectedMessage, exception.Message);
        }
    }
}
