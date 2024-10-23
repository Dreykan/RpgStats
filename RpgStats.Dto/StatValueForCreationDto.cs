using System.ComponentModel.DataAnnotations;

namespace RpgStats.Dto;

public class StatValueForCreationDto
{
    [Required(ErrorMessage = "A Level entry is required.")]
    public int? Level { get; set; }

    [Required(ErrorMessage = "A value for the stat entry is required.")]
    public int Value { get; set; }

    [Required(ErrorMessage = "A bonus contained herein in numbers for this entry is required.")]
    public int? ContainedBonusNum { get; set; }

    [Required(ErrorMessage = "A bonus contained herein in percent for this entry is required.")]
    public int? ContainedBonusPercent { get; set; }

}