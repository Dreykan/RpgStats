using System.ComponentModel.DataAnnotations;

namespace RpgStats.Dto;

public record CharacterForCreationDto
{
    [Required(ErrorMessage = "A name for a character is required.")]
    [StringLength(60, ErrorMessage = "The name can't be longer than 60 characters.")]
    public string Name { get; set; } = string.Empty;

    public byte[]? Picture { get; set; }

    [StringLength(2000, ErrorMessage = "The note can't be longer than 2000 characters.")]
    public string? Note { get; set; }
}