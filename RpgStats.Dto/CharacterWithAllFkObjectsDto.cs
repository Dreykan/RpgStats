namespace RpgStats.Dto;

public class CharacterWithAllFkObjectsDto
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public byte[]? Picture { get; set; }
    public GameWithoutFkObjectsDto? GameWithoutFkObjectsDto { get; set; }
    public IEnumerable<StatValueWithStatObjectDto>? StatValuesWithStatObjectDtos { get; set; }
}