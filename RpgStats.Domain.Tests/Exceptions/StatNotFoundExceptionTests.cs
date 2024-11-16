using RpgStats.Domain.Exceptions;

namespace RpgStats.Domain.Tests.Exceptions;

public class StatNotFoundExceptionTests
{
    [Fact]
    public void StatNotFoundException_Creation_Success()
    {
        const long statId = 456;

        var exception = new StatNotFoundException(statId);

        Assert.NotNull(exception);
    }

    [Fact]
    public void StatNotFoundException_Type_IsCorrect()
    {
        const long statId = 456;

        var exception = new StatNotFoundException(statId);

        Assert.IsType<StatNotFoundException>(exception);
    }

    [Fact]
    public void StatNotFoundException_Message_IsCorrect()
    {
        const long statId = 456;
        var expectedMessage = $"The Stat with the identifier {statId} was not found.";

        var exception = new StatNotFoundException(statId);

        Assert.Equal(expectedMessage, exception.Message);
    }
}