using System.ComponentModel.DataAnnotations;

namespace RpgStats.Dto;

public class PlatformForUpdateDto
{
    [Required(ErrorMessage = "A name for the platform is required.")]
    [StringLength(60, ErrorMessage = "The name for the platform can't be longer than 60 characters.")]
    public string Name { get; set; } = string.Empty;
}