using Microsoft.EntityFrameworkCore;
using RpgStats.Domain.Entities;
using RpgStats.Repo;

namespace RpgStats.Services.Tests;

public class DatabaseFixture : IDisposable
{
    public RpgStatsContext Context { get; }

    public DatabaseFixture()
    {
        var options = new DbContextOptionsBuilder<RpgStatsContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        Context = new RpgStatsContext(options);

        InitializeTestData();
    }

    private void InitializeTestData()
    {
        Context.Games.Add(new Game { Id = 1, Name = "Game1" });
        Context.Characters.Add(new Character { Id = 1, Name = "Test1", GameId = 1 });
        Context.Characters.Add(new Character { Id = 2, Name = "Test2", GameId = 2 });
        Context.Characters.Add(new Character { Id = 3, Name = "Test3", GameId = 2 });
        Context.Characters.Add(new Character { Id = 4, Name = "Char1", GameId = 3 });
        Context.Characters.Add(new Character { Id = 5, Name = "Char2", GameId = 4 });
        
        Context.SaveChanges();
    }

    public void Dispose()
    {
        Context.Dispose();
    }
}