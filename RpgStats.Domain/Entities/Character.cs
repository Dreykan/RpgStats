using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RpgStats.Domain.Entities;

[Table("Characters")]
public class Character
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("CharacterId")]
    public long Id { get; set; }

    [Required(ErrorMessage = "A name for a character is required.")]
    [StringLength(60, ErrorMessage = "The Name can't be longer than 60 characters.")]
    public string? Name { get; set; }

    public byte[]? Picture { get; set; }

    [Required(ErrorMessage = "An entry in the column GameId is required.")]
    [ForeignKey(nameof(Game))]
    public long GameId { get; set; }
    public Game? Game { get; set; }

    [InverseProperty("Character")]
    public ICollection<StatValue>? StatValues { get; set; }
}