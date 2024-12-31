using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RpgStats.Domain.Entities;

namespace RpgStats.Dto;

public class GameStatForCreationDto
{
    [Required(ErrorMessage = "An entry for the column SortIndex is required.")]
    public int SortIndex { get; set; }

    [Required(ErrorMessage = "An entry for the column CustomStatName is required.")]
    [StringLength(50, ErrorMessage = "The name cannot be longer than 50 characters.")]
    public string CustomStatName { get; set; } = string.Empty;

    [Required(ErrorMessage = "An entry for the column CustomStatShortName is required.")]
    [StringLength(8, ErrorMessage = "The shortname for the stat can't be longer than 8 characters.")]
    public string CustomStatShortName { get; set; } = string.Empty;

    [Required(ErrorMessage = "An entry for the column GameId is required.")]
    [ForeignKey(nameof(Game))]
    public long GameId { get; set; }

    [Required(ErrorMessage = "An entry for the column StatId is required.")]
    [ForeignKey(nameof(Stat))]
    public long StatId { get; set; }
}