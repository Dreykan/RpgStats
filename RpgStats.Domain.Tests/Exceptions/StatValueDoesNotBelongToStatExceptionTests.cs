using RpgStats.Domain.Exceptions;

namespace RpgStats.Domain.Tests.Exceptions;

public class StatValueDoesNotBelongToStatExceptionTests
{
    [Fact]
    public void StatValueDoesNotBelongToStatException_Creation_Success()
    {
        const long statvalueId = 123;
        const long statId = 456;

        var exception = new StatValueDoesNotBelongToStatException(statvalueId, statId);

        Assert.NotNull(exception);
    }
        
    [Fact]
    public void StatValueDoesNotBelongToStatException_Type_IsCorrect()
    {
        const long statvalueId = 123;
        const long statId = 456;

        var exception = new StatValueDoesNotBelongToStatException(statvalueId, statId);

        Assert.IsType<StatValueDoesNotBelongToStatException>(exception);
    }

    [Fact]
    public void StatValueDoesNotBelongToStatException_Message_IsCorrect()
    {
        const long statvalueId = 123;
        const long statId = 456;
        string expectedMessage = $"The StatValue with the identifier {statvalueId} does not belong to the Stat with the identifier {statId}.";

        var exception = new StatValueDoesNotBelongToStatException(statvalueId, statId);

        Assert.Equal(expectedMessage, exception.Message);
    }
}