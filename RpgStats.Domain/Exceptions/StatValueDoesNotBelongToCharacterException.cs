namespace RpgStats.Domain.Exceptions;

public sealed class StatValueDoesNotBelongToCharacterException : BadRequestException
{
    public StatValueDoesNotBelongToCharacterException(long statvalueId, long characterId)
        : base($"The StatValue with the identifier {statvalueId} does not belong to the Character with the identifier {characterId}.")
    {
        
    }
}