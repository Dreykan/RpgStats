namespace RpgStats.Dto;

public class CharacterWithoutFkObjectsDto
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public byte[]? Picture { get; set; }
}