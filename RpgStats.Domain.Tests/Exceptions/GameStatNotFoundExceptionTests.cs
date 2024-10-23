using RpgStats.Domain.Exceptions;

namespace RpgStats.Domain.Tests.Exceptions
{
    public class GameStatNotFoundExceptionTests
    {
        [Fact]
        public void GameStatNotFoundException_Creation_Success()
        {
            long gameStatId = 456;

            var exception = new GameStatNotFoundException(gameStatId);

            Assert.NotNull(exception);
        }
        
        [Fact]
        public void GameStatNotFoundException_Type_IsCorrect()
        {
            long gameStatId = 456;

            var exception = new GameStatNotFoundException(gameStatId);

            Assert.IsType<GameStatNotFoundException>(exception);
        }

        [Fact]
        public void GameStatNotFoundException_Message_IsCorrect()
        {
            long gameStatId = 456;
            string expectedMessage = $"The GameStat with the identifier {gameStatId} was not found.";

            var exception = new GameStatNotFoundException(gameStatId);

            Assert.Equal(expectedMessage, exception.Message);
        }
    }
}
