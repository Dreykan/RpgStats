using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RpgStats.Domain.Entities;

[Table("Stats")]
public class Stat
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("StatId")]
    public long Id { get; set; }

    [Required(ErrorMessage = "A name for the stat is required.")]
    [StringLength(50, ErrorMessage = "The name cannot be longer than 50 characters.")]
    public string Name { get; set; } = string.Empty;

    [StringLength(8, ErrorMessage = "The shortname for the stat can't be longer than 8 characters.")]
    public string? ShortName { get; set; }

    [InverseProperty("Stat")] public ICollection<StatValue>? StatValues { get; set; }

    [InverseProperty("Stat")] public ICollection<GameStat>? GameStats { get; set; }
}