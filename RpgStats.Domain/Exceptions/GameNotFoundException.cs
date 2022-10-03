namespace RpgStats.Domain.Exceptions;

public sealed class GameNotFoundException : NotFoundException
{
    public GameNotFoundException(long gameId) 
        : base($"The Game with the identifier {gameId} was not found.")
    {
        
    }
}