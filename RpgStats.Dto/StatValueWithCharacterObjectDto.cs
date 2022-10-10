namespace RpgStats.Dto;

public class StatValueWithCharacterObjectDto
{
    public long Id { get; set; }
    public int Level { get; set; }
    public int Value { get; set; }
    public CharacterWithoutFkObjectsDto? CharacterWithoutFkObjectsDto { get; set; }
}