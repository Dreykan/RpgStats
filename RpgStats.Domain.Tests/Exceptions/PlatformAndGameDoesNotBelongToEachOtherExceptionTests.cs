using RpgStats.Domain.Exceptions;

namespace RpgStats.Domain.Tests.Exceptions;

public class PlatformAndGameDoesNotBelongToEachOtherExceptionTests
{
    [Fact]
    public void PlatformAndGameDoesNotBelongToEachOtherException_Creation_Success()
    {
        const long platformId = 1;
        const long gameId = 2;

        var exception = new PlatformAndGameDoesNotBelongToEachOtherException(platformId, gameId);

        Assert.NotNull(exception);
    }
        
    [Fact]
    public void PlatformAndGameDoesNotBelongToEachOtherException_Type_IsCorrect()
    {
        const long platformId = 1;
        const long gameId = 2;

        var exception = new PlatformAndGameDoesNotBelongToEachOtherException(platformId, gameId);

        Assert.IsType<PlatformAndGameDoesNotBelongToEachOtherException>(exception);
    }

    [Fact]
    public void PlatformAndGameDoesNotBelongToEachOtherException_Message_IsCorrect()
    {
        const long platformId = 1;
        const long gameId = 2;
        string expectedMessage = $"The Game with the identifier {gameId} is in no relationship to the Platform with the identifier {platformId}.";

        var exception = new PlatformAndGameDoesNotBelongToEachOtherException(platformId, gameId);

        Assert.Equal(expectedMessage, exception.Message);
    }
}