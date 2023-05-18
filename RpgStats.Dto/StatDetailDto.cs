namespace RpgStats.Dto;

public class StatDetailDto
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? ShortName { get; set; }
    public IEnumerable<StatValueWithCharacterObjectDto>? StatValueWithCharacterObjectDtos { get; set; }
    public IEnumerable<GameWithoutFkObjectsDto>? GameWithoutFkObjectsDtos { get; set; }
}