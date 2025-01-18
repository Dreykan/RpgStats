using Microsoft.EntityFrameworkCore;
using RpgStats.Domain.Entities;

namespace RpgStats.Repo;

public class RpgStatsContext : DbContext
{
    public virtual DbSet<Character> Characters { get; set; } = null!;
    public DbSet<Game> Games { get; set; } = null!;
    public DbSet<GameStat> GameStats { get; set; } = null!;
    public DbSet<Platform> Platforms { get; set; } = null!;
    public DbSet<PlatformGame> PlatformGames { get; set; } = null!;
    public DbSet<Stat> Stats { get; set; } = null!;
    public DbSet<StatValue> StatValues { get; set; } = null!;

    public RpgStatsContext(DbContextOptions<RpgStatsContext> options) : base(options)
    {
    }
}