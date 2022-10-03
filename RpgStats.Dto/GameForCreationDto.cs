using System.ComponentModel.DataAnnotations;

namespace RpgStats.Dto;

public class GameForCreationDto
{
    [Required(ErrorMessage = "A name for the game is required.")]
    [StringLength(100, ErrorMessage = "The name for the game can't be longer than 100 characters.")]
    public string? Name { get; set; }
    public byte[]? Picture { get; set; }
}