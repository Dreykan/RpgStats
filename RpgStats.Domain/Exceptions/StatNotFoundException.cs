namespace RpgStats.Domain.Exceptions;

public sealed class StatNotFoundException : NotFoundException
{
    public StatNotFoundException(long statId)
        : base($"The Stat with the identifier {statId} was not found.")
    {
    }
}