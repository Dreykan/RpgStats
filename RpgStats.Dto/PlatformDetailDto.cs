namespace RpgStats.Dto;

public class PlatformDetailDto
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public IEnumerable<GameWithoutFkObjectsDto>? GameWithoutFkObjectsDtos { get; set; }
}