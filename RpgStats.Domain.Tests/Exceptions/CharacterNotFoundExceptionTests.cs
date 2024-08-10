using RpgStats.Domain.Exceptions;

namespace RpgStats.Domain.Tests.Exceptions
{
    public class CharacterNotFoundExceptionTests
    {
        [Fact]
        public void CharacterNotFoundException_Creation_Success()
        {
            // Arrange
            long characterId = 123;

            // Act
            var exception = new CharacterNotFoundException(characterId);

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<CharacterNotFoundException>(exception);
        }

        [Fact]
        public void CharacterNotFoundException_Message_CorrectFormat()
        {
            // Arrange
            long characterId = 123;
            string expectedMessage = $"The Character with the identifier {characterId} was not found.";

            // Act
            var exception = new CharacterNotFoundException(characterId);

            // Assert
            Assert.Equal(expectedMessage, exception.Message);
        }
    }
}
