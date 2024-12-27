namespace RpgStats.Dto;

public record StatValueWithCharacterObjectDto
{
    public long Id { get; set; }
    public int Level { get; set; }
    public int Value { get; set; }
    public int ContainedBonusNum { get; set; }
    public int ContainedBonusPercent { get; set; }
    public CharacterWithoutFkObjectsDto? CharacterWithoutFkObjectsDto { get; set; }
}