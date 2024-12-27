namespace RpgStats.Dto;

public record PlatformWithoutFkObjectsDto
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
}