namespace RpgStats.Domain.Exceptions;

public sealed class PlatformGameNotFoundException : NotFoundException
{
    public PlatformGameNotFoundException(long platformGameId)
        : base($"The PlatformGame with the identifier {platformGameId} was not found.")
    {
    }
}