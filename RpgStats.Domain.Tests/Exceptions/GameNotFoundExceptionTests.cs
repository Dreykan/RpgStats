using RpgStats.Domain.Exceptions;

namespace RpgStats.Domain.Tests.Exceptions
{
    public class GameNotFoundExceptionTests
    {
        [Fact]
        public void GameNotFoundException_Creation_Success()
        {
            long gameId = 456;

            var exception = new GameNotFoundException(gameId);

            Assert.NotNull(exception);
        }
        
        [Fact]
        public void GameNotFoundException_Type_IsCorrect()
        {
            long gameId = 456;

            var exception = new GameNotFoundException(gameId);

            Assert.IsType<GameNotFoundException>(exception);
        }

        [Fact]
        public void GameNotFoundException_Message_IsCorrect()
        {
            long gameId = 456;
            string expectedMessage = $"The Game with the identifier {gameId} was not found.";

            var exception = new GameNotFoundException(gameId);

            Assert.Equal(expectedMessage, exception.Message);
        }
    }
}
