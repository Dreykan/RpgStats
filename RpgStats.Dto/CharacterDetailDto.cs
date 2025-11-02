namespace RpgStats.Dto;

public record CharacterDetailDto
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public byte[]? Picture { get; set; }
    public GameDto? Game { get; set; }
    public List<GameStatDto>? GameStats { get; set; }
    public List<StatValueDto>? StatValues { get; set; }
}