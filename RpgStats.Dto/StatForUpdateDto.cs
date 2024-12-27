using System.ComponentModel.DataAnnotations;

namespace RpgStats.Dto;

public record StatForUpdateDto
{
    [Required(ErrorMessage = "A name for the stat is required.")]
    [StringLength(50, ErrorMessage = "The name for the stat can't be longer than 50 characters.")]
    public string Name { get; set; } = string.Empty;

    [StringLength(8, ErrorMessage = "The shortname for the stat can't be longer than 8 characters.")]
    public string? ShortName { get; set; }
}