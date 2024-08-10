using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RpgStats.Domain.Entities;

[Table("StatValues")]
public class StatValue
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("StatValueId")]
    public long Id { get; set; }

    [Required(ErrorMessage = "A Level entry is required.")]
    public int? Level { get; set; }

    [Required(ErrorMessage = "An Character-Entry is required.")]
    [ForeignKey(nameof(Character))]
    public long? CharacterId { get; set; }
    public Character? Character { get; set; }

    [Required(ErrorMessage = "A Stat-Entry is required.")]
    [ForeignKey(nameof(Stat))]
    public long? StatId { get; set; }
    public Stat? Stat { get; set; }

    [Required(ErrorMessage = "A value for the stat entry is required.")]
    public int? Value { get; set; }

    [Required(ErrorMessage = "A bonus contained herein in numbers for this entry is required.")]
    public int? ContainedBonusNum { get; set; }

    [Required(ErrorMessage = "A bonus contained herein in percent for this entry is required.")]
    public int? ContainedBonusPercent { get; set; }

}