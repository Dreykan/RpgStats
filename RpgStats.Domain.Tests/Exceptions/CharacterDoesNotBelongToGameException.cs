using RpgStats.Domain.Exceptions;
using Xunit;

namespace RpgStats.Domain.Tests.Exceptions
{
    public class CharacterDoesNotBelongToGameExceptionTests
    {
        [Fact]
        public void Exception_Message_Is_Correct()
        {
            // Arrange
            long characterId = 1;
            long gameId = 2;
            var expectedMessage = $"The Character with the identifier {characterId} does not belong to the game with the identifier {gameId}";

            // Act
            var exception = new CharacterDoesNotBelongToGameException(characterId, gameId);

            // Assert
            Assert.Equal(expectedMessage, exception.Message);
        }

        [Fact]
        public void Exception_Type_Is_Correct()
        {
            // Arrange
            long characterId = 1;
            long gameId = 2;

            // Act
            var exception = new CharacterDoesNotBelongToGameException(characterId, gameId);

            // Assert
            Assert.IsType<CharacterDoesNotBelongToGameException>(exception);
        }
    }
}
