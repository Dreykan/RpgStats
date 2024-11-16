namespace RpgStats.Domain.Exceptions;

public sealed class PlatformAndGameDoesNotBelongToEachOtherException : BadRequestException
{
    public PlatformAndGameDoesNotBelongToEachOtherException(long platformId, long gameId)
        : base(
            $"The Game with the identifier {gameId} is in no relationship to the Platform with the identifier {platformId}.")
    {
    }
}