namespace RpgStats.Dto;

public record StatWithoutFkObjectsDto
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? ShortName { get; set; }
}