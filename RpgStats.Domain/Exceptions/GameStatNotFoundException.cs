namespace RpgStats.Domain.Exceptions;

public class GameStatNotFoundException : NotFoundException
{
    public GameStatNotFoundException(long gameStatId) : base($"The GameStat with the identifier {gameStatId} was not found.")
    {
    }
}