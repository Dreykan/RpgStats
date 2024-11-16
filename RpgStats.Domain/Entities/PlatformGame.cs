using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RpgStats.Domain.Entities;

[Table("PlatformsGames")]
public class PlatformGame
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("PlatformGameId")]
    public long Id { get; set; }

    [Required(ErrorMessage = "An entry for column PlatformId is required.")]
    [ForeignKey(nameof(Platform))]
    public long PlatformId { get; set; }

    public Platform? Platform { get; set; }

    [Required(ErrorMessage = "An entry for the column GameId is required.")]
    [ForeignKey(nameof(Game))]
    public long GameId { get; set; }

    public Game? Game { get; set; }
}