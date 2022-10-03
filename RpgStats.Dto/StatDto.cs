namespace RpgStats.Dto;

public class StatDto
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? ShortName { get; set; }
    public IEnumerable<StatValueDto>? StatValues { get; set; }
}