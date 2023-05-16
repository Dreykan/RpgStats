namespace RpgStats.Dto;

public class StatValueWithStatObjectDto
{
    public long Id { get; set; }
    public int Level { get; set; }
    public StatWithoutFkObjectsDto? StatWithoutFkObjectsDto { get; set; }
    public int Value { get; set; }
    public int ContainedBonusNum { get; set; }
    public int ContainedBonusPercent { get; set; }

}