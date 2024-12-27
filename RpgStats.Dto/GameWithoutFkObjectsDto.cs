namespace RpgStats.Dto;

public record GameWithoutFkObjectsDto
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public byte[]? Picture { get; set; }
}