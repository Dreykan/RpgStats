﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RpgStats.Domain.Entities;

[Table("Games")]
public class Game
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("GameId")]
    public long Id { get; set; }

    [Required(ErrorMessage = "A name for the game is required.")]
    [StringLength(100, ErrorMessage = "The name for the game can't be longer than 100 characters.")]
    public string Name { get; set; } = string.Empty;

    public byte[]? Picture { get; set; }

    [InverseProperty("Game")] public ICollection<PlatformGame>? PlatformGames { get; set; }

    [InverseProperty("Game")] public ICollection<GameStat>? GameStats { get; set; }

    [InverseProperty("Game")] public ICollection<Character>? Characters { get; set; }
}