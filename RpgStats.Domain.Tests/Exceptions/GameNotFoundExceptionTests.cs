using RpgStats.Domain.Exceptions;

namespace RpgStats.Domain.Tests.Exceptions
{
    public class GameNotFoundExceptionTests
    {
        [Fact]
        public void GameNotFoundException_Creation_Success()
        {
            // Arrange
            long gameId = 456;

            // Act
            var exception = new GameNotFoundException(gameId);

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<GameNotFoundException>(exception);
        }

        [Fact]
        public void GameNotFoundException_Message_CorrectFormat()
        {
            // Arrange
            long gameId = 456;
            string expectedMessage = $"The Game with the identifier {gameId} was not found.";

            // Act
            var exception = new GameNotFoundException(gameId);

            // Assert
            Assert.Equal(expectedMessage, exception.Message);
        }
    }
}
