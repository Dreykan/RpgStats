using RpgStats.Domain.Exceptions;

namespace RpgStats.Domain.Tests.Exceptions
{
    public class StatValueDoesNotBelongToCharacterExceptionTests
    {
        [Fact]
        public void StatValueDoesNotBelongToCharacterException_Creation_Success()
        {
            long statvalueId = 456;
            long characterId = 789;

            var exception = new StatValueDoesNotBelongToCharacterException(statvalueId, characterId);

            Assert.NotNull(exception);
        }
        
        [Fact]
        public void StatValueDoesNotBelongToCharacterException_Type_IsCorrect()
        {
            long statvalueId = 456;
            long characterId = 789;

            var exception = new StatValueDoesNotBelongToCharacterException(statvalueId, characterId);

            Assert.IsType<StatValueDoesNotBelongToCharacterException>(exception);
        }

        [Fact]
        public void StatValueDoesNotBelongToCharacterException_Message_IsCorrect()
        {
            long statvalueId = 456;
            long characterId = 789;
            string expectedMessage = $"The StatValue with the identifier {statvalueId} does not belong to the Character with the identifier {characterId}.";

            var exception = new StatValueDoesNotBelongToCharacterException(statvalueId, characterId);

            Assert.Equal(expectedMessage, exception.Message);
        }
    }
}
