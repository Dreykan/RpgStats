namespace RpgStats.Dto;

public record GameDetailDto
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public byte[]? Picture { get; set; }
    public IEnumerable<CharacterWithoutFkObjectsDto>? CharacterWithoutFkObjectsDtos { get; set; }
    public IEnumerable<StatWithoutFkObjectsDto>? StatWithoutFkObjectsDtos { get; set; }
    public IEnumerable<PlatformWithoutFkObjectsDto>? PlatformWithoutFkObjectsDtos { get; set; }
}