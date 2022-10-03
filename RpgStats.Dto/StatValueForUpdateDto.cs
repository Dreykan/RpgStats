using System.ComponentModel.DataAnnotations;

namespace RpgStats.Dto;

public class StatValueForUpdateDto
{
    [Required(ErrorMessage = "A Level entry is required.")]
    public int Level { get; set; }

    [Required(ErrorMessage = "A value for the stat entry is required.")]
    public int Value { get; set; }
}