using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RpgStats.Domain.Entities;

[Table("GamesStats")]
public class GameStat
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("GameStatId")]
    public long Id { get; set; }

    [Required(ErrorMessage = "An entry for the column GameId is required.")]
    [ForeignKey(nameof(Game))]
    public long GameId { get; set; }

    public Game? Game { get; set; }

    [Required(ErrorMessage = "An entry for the column StatId is required.")]
    [ForeignKey(nameof(Stat))]
    public long StatId { get; set; }

    public Stat? Stat { get; set; }
}