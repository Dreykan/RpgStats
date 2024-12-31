namespace RpgStats.Dto;

public record GameStatDto
{
    public long Id { get; set; }
    public int SortIndex { get; set; }
    public string CustomStatName { get; set; } = string.Empty;
    public string CustomStatShortName { get; set; } = string.Empty;
    public long GameId { get; set; }
    public long StatId { get; set; }
}