using RpgStats.Domain.Exceptions;

namespace RpgStats.Domain.Tests.Exceptions
{
    public class CharacterDoesNotBelongToGameExceptionTests
    {
        [Fact]
        public void CharacterDoesNotBelongToGameException_Creation_Success()
        {
            long characterId = 1;
            long gameId = 2;

            var exception = new CharacterDoesNotBelongToGameException(characterId, gameId);

            Assert.NotNull(exception);
        }
        
        [Fact]
        public void CharacterDoesNotBelongToGameException_Type_IsCorrect()
        {
            long characterId = 1;
            long gameId = 2;

            var exception = new CharacterDoesNotBelongToGameException(characterId, gameId);

            Assert.IsType<CharacterDoesNotBelongToGameException>(exception);
        }

        [Fact]
        public void CharacterDoesNotBelongToGameException_Message_IsCorrect()
        {
            long characterId = 1;
            long gameId = 2;
            var expectedMessage = $"The Character with the identifier {characterId} does not belong to the game with the identifier {gameId}";

            var exception = new CharacterDoesNotBelongToGameException(characterId, gameId);

            Assert.Equal(expectedMessage, exception.Message);
        }
    }
}