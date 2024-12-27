using System.ComponentModel.DataAnnotations;

namespace RpgStats.Dto;

public record CharacterForUpdateDto
{
    [Required(ErrorMessage = "A name for a character is required.")]
    [StringLength(60, ErrorMessage = "The name can't be longer than 60 characters.")]
    public string Name { get; set; } = string.Empty;

    public byte[]? Picture { get; set; }
}