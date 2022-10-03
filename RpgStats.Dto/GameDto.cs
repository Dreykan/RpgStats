namespace RpgStats.Dto;

public class GameDto
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public byte[]? Picture { get; set; }
    public IEnumerable<PlatformGameDto>? PlatformGames { get; set; }
    public IEnumerable<CharacterDto>? Characters { get; set; }
}