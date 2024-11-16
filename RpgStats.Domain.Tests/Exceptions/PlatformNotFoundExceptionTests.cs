using RpgStats.Domain.Exceptions;

namespace RpgStats.Domain.Tests.Exceptions;

public class PlatformNotFoundExceptionTests
{
    [Fact]
    public void PlatformNotFoundException_Creation_Success()
    {
        const long platformId = 456;

        var exception = new PlatformNotFoundException(platformId);

        Assert.NotNull(exception);
    }

    [Fact]
    public void PlatformNotFoundException_Type_IsCorrect()
    {
        const long platformId = 456;

        var exception = new PlatformNotFoundException(platformId);

        Assert.IsType<PlatformNotFoundException>(exception);
    }

    [Fact]
    public void PlatformNotFoundException_Message_IsCorrect()
    {
        const long platformId = 456;
        var expectedMessage = $"The Platform with the identifier {platformId} was not found.";

        var exception = new PlatformNotFoundException(platformId);

        Assert.Equal(expectedMessage, exception.Message);
    }
}