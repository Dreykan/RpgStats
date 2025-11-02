using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RpgStats.Domain.Entities;

namespace RpgStats.Dto;

public class PlatformGameForCreationDto
{
    [Required(ErrorMessage = "An entry for the column PlatformId is required")]
    [ForeignKey(nameof(Platform))]
    public long PlatformId { get; set; }

    [Required(ErrorMessage = "An entry for the column GameId is required")]
    [ForeignKey(nameof(Game))]
    public long GameId { get; set; }
}