using RpgStats.Domain.Exceptions;

namespace RpgStats.Domain.Tests.Exceptions;

public class CharacterNotFoundExceptionTests
{
    [Fact]
    public void CharacterNotFoundException_Creation_Success()
    {
        const long characterId = 123;

        var exception = new CharacterNotFoundException(characterId);

        Assert.NotNull(exception);
    }

    [Fact]
    public void CharacterNotFoundException_Type_IsCorrect()
    {
        const long characterId = 123;

        var exception = new CharacterNotFoundException(characterId);

        Assert.IsType<CharacterNotFoundException>(exception);
    }

    [Fact]
    public void CharacterNotFoundException_Message_IsCorrect()
    {
        const long characterId = 123;
        var expectedMessage = $"The Character with the identifier {characterId} was not found.";

        var exception = new CharacterNotFoundException(characterId);

        Assert.Equal(expectedMessage, exception.Message);
    }
}