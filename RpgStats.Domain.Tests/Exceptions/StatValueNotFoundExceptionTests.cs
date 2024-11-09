using RpgStats.Domain.Exceptions;

namespace RpgStats.Domain.Tests.Exceptions;

public class StatValueNotFoundExceptionTests
{
    [Fact]
    public void StatValueNotFoundException_Creation_Success()
    {
        const long statValueId = 456;

        var exception = new StatValueNotFoundException(statValueId);

        Assert.NotNull(exception);
    }
        
    [Fact]
    public void StatValueNotFoundException_Type_IsCorrect()
    {
        const long statValueId = 456;

        var exception = new StatValueNotFoundException(statValueId);

        Assert.IsType<StatValueNotFoundException>(exception);
    }

    [Fact]
    public void StatValueNotFoundException_Message_IsCorrect()
    {
        const long statValueId = 456;
        string expectedMessage = $"The StatValue with the identifier {statValueId} was not found.";

        var exception = new StatValueNotFoundException(statValueId);

        Assert.Equal(expectedMessage, exception.Message);
    }
}