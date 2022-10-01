using Microsoft.EntityFrameworkCore;
using RpgStats.Domain.Entities;

namespace RpgStats.Repo;

public class RpgStatsContext : DbContext
{
    private readonly string _connectionString = string.Empty;

    public RpgStatsContext()
    {
        
    }

    public RpgStatsContext(string connectionString)
    {
        _connectionString = connectionString;
    }

    public RpgStatsContext(DbContextOptions<RpgStatsContext> options) : base(options)
    {
        
    }

    public DbSet<Character> Characters { get; set; } = null!;
    public DbSet<Game> Games { get; set; } = null!;
    public DbSet<Platform> Platforms { get; set; } = null!;
    public DbSet<PlatformGame> PlatformGames { get; set; } = null!;
    public DbSet<Stat> Stats { get; set; } = null!;
    public DbSet<StatValue> StatValues { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(RpgStatsContext).Assembly);
    }
}