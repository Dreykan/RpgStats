using RpgStats.Domain.Exceptions;

namespace RpgStats.Domain.Tests.Exceptions;

public class StatValueDoesNotBelongToCharacterExceptionTests
{
    [Fact]
    public void StatValueDoesNotBelongToCharacterException_Creation_Success()
    {
        const long statvalueId = 456;
        const long characterId = 789;

        var exception = new StatValueDoesNotBelongToCharacterException(statvalueId, characterId);

        Assert.NotNull(exception);
    }

    [Fact]
    public void StatValueDoesNotBelongToCharacterException_Type_IsCorrect()
    {
        const long statvalueId = 456;
        const long characterId = 789;

        var exception = new StatValueDoesNotBelongToCharacterException(statvalueId, characterId);

        Assert.IsType<StatValueDoesNotBelongToCharacterException>(exception);
    }

    [Fact]
    public void StatValueDoesNotBelongToCharacterException_Message_IsCorrect()
    {
        const long statvalueId = 456;
        const long characterId = 789;
        var expectedMessage =
            $"The StatValue with the identifier {statvalueId} does not belong to the Character with the identifier {characterId}.";

        var exception = new StatValueDoesNotBelongToCharacterException(statvalueId, characterId);

        Assert.Equal(expectedMessage, exception.Message);
    }
}