namespace RpgStats.Dto;

public record StatDto
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? ShortName { get; set; }
    public IEnumerable<StatValueDto>? StatValues { get; set; }
    public IEnumerable<GameStatDto>? GameStats { get; set; }
}