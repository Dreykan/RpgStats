namespace RpgStats.Dto;

public record StatDetailDto
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? ShortName { get; set; }
    public IEnumerable<StatValueWithCharacterObjectDto>? StatValueWithCharacterObjectDtos { get; set; }
    public IEnumerable<GameWithoutFkObjectsDto>? GameWithoutFkObjectsDtos { get; set; }
}