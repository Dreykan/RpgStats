namespace RpgStats.Dto;

public record GameDto
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public byte[]? Picture { get; set; }
    public IEnumerable<PlatformGameDto>? PlatformGames { get; set; }
    public IEnumerable<GameStatDto>? GameStat { get; set; }
    public IEnumerable<CharacterDto>? Characters { get; set; }
}