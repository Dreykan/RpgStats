namespace RpgStats.Dto;

public record PlatformWithGamesDto
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public IEnumerable<GameDto>? GameDtos { get; set; }
}