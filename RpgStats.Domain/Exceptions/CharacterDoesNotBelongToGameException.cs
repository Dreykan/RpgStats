namespace RpgStats.Domain.Exceptions;

public sealed class CharacterDoesNotBelongToGameException : BadRequestException
{
    public CharacterDoesNotBelongToGameException(long characterId, long gameId)
        : base(
            $"The Character with the identifier {characterId} does not belong to the game with the identifier {gameId}")
    {
    }
}