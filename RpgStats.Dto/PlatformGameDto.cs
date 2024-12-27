namespace RpgStats.Dto;

public record PlatformGameDto
{
    public long Id { get; set; }
    public long PlatformId { get; set; }
    public long GameId { get; set; }
}