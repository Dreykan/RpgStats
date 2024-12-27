using System.ComponentModel.DataAnnotations;

namespace RpgStats.Dto;

public record GameForUpdateDto
{
    [Required(ErrorMessage = "A name for the game is required.")]
    [StringLength(100, ErrorMessage = "The name for the game can't be longer than 100 characters.")]
    public string Name { get; set; } = string.Empty;

    public byte[]? Picture { get; set; }
}