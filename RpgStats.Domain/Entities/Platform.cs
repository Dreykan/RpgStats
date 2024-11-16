using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RpgStats.Domain.Entities;

[Table("Platforms")]
public class Platform
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("PlatformId")]
    public long Id { get; set; }

    [Required(ErrorMessage = "A name for the platform is required.")]
    [StringLength(60, ErrorMessage = "The name for the platform can't be longer than 60 characters.")]
    public string Name { get; set; } = string.Empty;

    [InverseProperty("Platform")] public ICollection<PlatformGame>? PlatformGames { get; set; }
}