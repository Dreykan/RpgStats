namespace RpgStats.Dto;

public class PlatformDto
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public IEnumerable<PlatformGameDto>? PlatformGames { get; set; }

}