using RpgStats.Domain.Exceptions;

namespace RpgStats.Domain.Tests.Exceptions;

public class GameNotFoundExceptionTests
{
    [Fact]
    public void GameNotFoundException_Creation_Success()
    {
        const long gameId = 456;

        var exception = new GameNotFoundException(gameId);

        Assert.NotNull(exception);
    }

    [Fact]
    public void GameNotFoundException_Type_IsCorrect()
    {
        const long gameId = 456;

        var exception = new GameNotFoundException(gameId);

        Assert.IsType<GameNotFoundException>(exception);
    }

    [Fact]
    public void GameNotFoundException_Message_IsCorrect()
    {
        const long gameId = 456;
        var expectedMessage = $"The Game with the identifier {gameId} was not found.";

        var exception = new GameNotFoundException(gameId);

        Assert.Equal(expectedMessage, exception.Message);
    }
}