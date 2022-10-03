namespace RpgStats.Domain.Exceptions;

public sealed class PlatformNotFoundException : NotFoundException
{
    public PlatformNotFoundException(long platformId) 
        : base($"The Platform with the identifier {platformId} was not found.")
    {
        
    }
}