using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

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
    public string? Name { get; set; }

    [StringLength(8, ErrorMessage = "The shortname for the stat can't be longer than 8 characters.")]
    public string? ShortName { get; set; }

    public ICollection<StatValue>? StatValues { get; set; }
}