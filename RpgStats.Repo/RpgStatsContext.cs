using Microsoft.EntityFrameworkCore;
using RpgStats.Domain.Entities;
using RpgStats.Repo.Seeds;

namespace RpgStats.Repo;

public class RpgStatsContext : DbContext
{
    private readonly string _connectionString = string.Empty;

    public RpgStatsContext(DbContextOptions<RpgStatsContext> options) : base(options)
    {
        
    }

    public DbSet<Character> Characters { get; set; } = null!;
    public DbSet<Game> Games { get; set; } = null!;
    public DbSet<GameStat> GameStats { get; set; } = null!;
    public DbSet<Platform> Platforms { get; set; } = null!;
    public DbSet<PlatformGame> PlatformGames { get; set; } = null!;
    public DbSet<Stat> Stats { get; set; } = null!;
    public DbSet<StatValue> StatValues { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PlatformGame>().HasKey(pg => new {pg.PlatformId, pg.GameId});
        modelBuilder.Entity<GameStat>().HasKey(stat => new {stat.GameId, stat.StatId});
        modelBuilder.Entity<StatValue>().HasKey(sv => new {sv.CharacterId, sv.StatId});


        modelBuilder.ApplyConfigurationsFromAssembly(typeof(RpgStatsContext).Assembly);

        if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
        {
            SeedData(modelBuilder);
        }
    }

    private void SeedData(ModelBuilder modelBuilder)
    {
        new CharacterConfiguration().Configure(modelBuilder.Entity<Character>());
        new GameConfiguration().Configure(modelBuilder.Entity<Game>());
        new GameStatConfiguration().Configure(modelBuilder.Entity<GameStat>());
        new PlatformConfiguration().Configure(modelBuilder.Entity<Platform>());
        new PlatformGameConfiguration().Configure(modelBuilder.Entity<PlatformGame>());
        new StatConfiguration().Configure(modelBuilder.Entity<Stat>());
        new StatValueConfiguration().Configure(modelBuilder.Entity<StatValue>());
    }
}