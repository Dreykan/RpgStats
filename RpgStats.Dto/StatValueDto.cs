namespace RpgStats.Dto;

public class StatValueDto
{
    public long Id { get; set; }
    public int Level { get; set; }
    public long CharacterId { get; set; }
    public long StatId { get; set; }
    public int Value { get; set; }
    public int ContainedBonusNum { get; set; }
    public int ContainedBonusPercent { get; set; }

}