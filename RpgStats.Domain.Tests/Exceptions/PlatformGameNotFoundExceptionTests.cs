using RpgStats.Domain.Exceptions;

namespace RpgStats.Domain.Tests.Exceptions;

public class PlatformGameNotFoundExceptionTests
{
    [Fact]
    public void PlatformGameNotFoundException_Creation_Success()
    {
        const long platformGameId = 456;

        var exception = new PlatformGameNotFoundException(platformGameId);

        Assert.NotNull(exception);
    }
        
    [Fact]
    public void PlatformGameNotFoundException_Type_IsCorrect()
    {
        const long platformGameId = 456;

        var exception = new PlatformGameNotFoundException(platformGameId);

        Assert.IsType<PlatformGameNotFoundException>(exception);
    }

    [Fact]
    public void PlatformGameNotFoundException_Message_IsCorrect()
    {
        const long platformGameId = 456;
        string expectedMessage = $"The PlatformGame with the identifier {platformGameId} was not found.";

        var exception = new PlatformGameNotFoundException(platformGameId);

        Assert.Equal(expectedMessage, exception.Message);
    }
}