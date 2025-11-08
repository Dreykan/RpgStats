namespace RpgStats.Dto;

public record CharacterDto
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public byte[]? Picture { get; set; }
    public string? Note { get; set; }
    public long GameId { get; set; }
    public IEnumerable<StatValueDto>? StatValues { get; set; }
}