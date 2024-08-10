using RpgStats.Domain.Exceptions;

namespace RpgStats.Domain.Tests.Exceptions
{
    public class GameStatNotFoundExceptionTests
    {
        [Fact]
        public void GameStatNotFoundException_Creation_Success()
        {
            // Arrange
            long gameStatId = 456;

            // Act
            var exception = new GameStatNotFoundException(gameStatId);

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<GameStatNotFoundException>(exception);
        }

        [Fact]
        public void GameStatNotFoundException_Message_CorrectFormat()
        {
            // Arrange
            long gameStatId = 456;
            string expectedMessage = $"The GameStat with the identifier {gameStatId} was not found.";

            // Act
            var exception = new GameStatNotFoundException(gameStatId);

            // Assert
            Assert.Equal(expectedMessage, exception.Message);
        }
    }
}
