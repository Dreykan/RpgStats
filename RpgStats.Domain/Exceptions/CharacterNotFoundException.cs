namespace RpgStats.Domain.Exceptions;

public sealed class CharacterNotFoundException : NotFoundException
{
    public CharacterNotFoundException(long characterId)
        : base($"The Character with the identifier {characterId} was not found.")
    {
        
    }
}