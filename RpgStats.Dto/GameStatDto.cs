namespace RpgStats.Dto;

public record GameStatDto
{
    public long Id { get; set; }
    public int SortIndex { get; set; }
    public long GameId { get; set; }
    public long StatId { get; set; }
}