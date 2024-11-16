using Microsoft.EntityFrameworkCore;

namespace RpgStats.Repo.Tests;

public class DatabaseMigrationTests
{
    [Fact]
    public async Task CanApplyMigrations()
    {
        var options = new DbContextOptionsBuilder<RpgStatsContext>()
            .UseSqlite("Filename=:memory:")
            .Options;

        await using var context = new RpgStatsContext(options);

        await context.Database.OpenConnectionAsync();
        await context.Database.MigrateAsync();

        Assert.True(await context.Database.CanConnectAsync());

        await context.Database.CloseConnectionAsync();
    }
}