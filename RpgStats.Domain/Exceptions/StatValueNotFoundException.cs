namespace RpgStats.Domain.Exceptions;

public sealed class StatValueNotFoundException : NotFoundException
{
    public StatValueNotFoundException(long statValueId) 
        : base($"The StatValue with the identifier {statValueId} was not found.")
    {
        
    }
}