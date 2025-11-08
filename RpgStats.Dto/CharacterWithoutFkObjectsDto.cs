namespace RpgStats.Dto;

public record CharacterWithoutFkObjectsDto
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public byte[]? Picture { get; set; }
    public string? Note { get; set; }
}