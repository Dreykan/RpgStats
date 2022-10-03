namespace RpgStats.Domain.Exceptions;

public sealed class StatValueDoesNotBelongToStatException : BadRequestException
{
    public StatValueDoesNotBelongToStatException(long statvalueId, long statId)
        : base($"The StatValue with the identifier {statvalueId} does not belong to the Stat with the identifier {statId}.")
    {
        
    }
}