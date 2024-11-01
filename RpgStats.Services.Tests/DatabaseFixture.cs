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
        AddCharacters();
        AddGames();
        AddPlatforms();
        AddStats();
        AddGameStats();
        AddPlatformGames();
        AddStatValues();
        
        Context.SaveChanges();
    }

    private void AddCharacters()
    {
        Context.Characters.Add(new Character { Id = 1, Name = "Test1", GameId = 1 });
        Context.Characters.Add(new Character { Id = 2, Name = "Test2", GameId = 2 });
        Context.Characters.Add(new Character { Id = 3, Name = "Test3", GameId = 2 });
        Context.Characters.Add(new Character { Id = 4, Name = "Char1", GameId = 3 });
        Context.Characters.Add(new Character { Id = 5, Name = "Char2", GameId = 4 });
    }

    private void AddGames()
    {
        Context.Games.Add(new Game { Id = 1, Name = "Game1" });
        Context.Games.Add(new Game { Id = 2, Name = "Game2" });
        Context.Games.Add(new Game { Id = 3, Name = "Game3" });
        Context.Games.Add(new Game { Id = 4, Name = "Game4" });
        Context.Games.Add(new Game { Id = 5, Name = "GoodGame1" });
        Context.Games.Add(new Game { Id = 6, Name = "GoodGame2" });
    }

    private void AddPlatforms()
    {
        Context.Platforms.Add(new Platform { Id = 1, Name = "Platform1" });
        Context.Platforms.Add(new Platform { Id = 2, Name = "Platform2" });
        Context.Platforms.Add(new Platform { Id = 3, Name = "Platform3" });
        Context.Platforms.Add(new Platform { Id = 4, Name = "Platform4" });
        Context.Platforms.Add(new Platform { Id = 5, Name = "GoodPlatform1" });
        Context.Platforms.Add(new Platform { Id = 6, Name = "GoodPlatform2" });
    }

    private void AddStats()
    {
        Context.Stats.Add(new Stat { Id = 1, Name = "StatValue1", ShortName = "SV1"});
        Context.Stats.Add(new Stat { Id = 2, Name = "StatValue2", ShortName = "SV2"});
        Context.Stats.Add(new Stat { Id = 3, Name = "StatValue3", ShortName = "SV3"});
        Context.Stats.Add(new Stat { Id = 4, Name = "StatValue4", ShortName = "SV4"});
        Context.Stats.Add(new Stat { Id = 5, Name = "GoodStatValue1", ShortName = "GSV1"});
        Context.Stats.Add(new Stat { Id = 6, Name = "GoodStatValue2", ShortName = "GSV2"});
    }

    private void AddGameStats()
    {
        Context.GameStats.Add(new GameStat { GameId = 1, StatId = 1 });
        Context.GameStats.Add(new GameStat { GameId = 1, StatId = 2 });
        Context.GameStats.Add(new GameStat { GameId = 1, StatId = 3 });
        Context.GameStats.Add(new GameStat { GameId = 1, StatId = 4 });
        Context.GameStats.Add(new GameStat { GameId = 2, StatId = 1 });
        Context.GameStats.Add(new GameStat { GameId = 2, StatId = 2 });
        Context.GameStats.Add(new GameStat { GameId = 2, StatId = 3 });
        Context.GameStats.Add(new GameStat { GameId = 2, StatId = 5 });
        Context.GameStats.Add(new GameStat { GameId = 3, StatId = 1 });
        Context.GameStats.Add(new GameStat { GameId = 3, StatId = 2 });
        Context.GameStats.Add(new GameStat { GameId = 3, StatId = 5 });
        Context.GameStats.Add(new GameStat { GameId = 3, StatId = 6 });
    }

    private void AddPlatformGames()
    {
        Context.PlatformGames.Add(new PlatformGame { PlatformId = 1, GameId = 1 });
        Context.PlatformGames.Add(new PlatformGame { PlatformId = 1, GameId = 2 });
        Context.PlatformGames.Add(new PlatformGame { PlatformId = 1, GameId = 3 });
        Context.PlatformGames.Add(new PlatformGame { PlatformId = 1, GameId = 4 });
        Context.PlatformGames.Add(new PlatformGame { PlatformId = 2, GameId = 1 });
        Context.PlatformGames.Add(new PlatformGame { PlatformId = 2, GameId = 2 });
        Context.PlatformGames.Add(new PlatformGame { PlatformId = 2, GameId = 3 });
        Context.PlatformGames.Add(new PlatformGame { PlatformId = 2, GameId = 5 });
        Context.PlatformGames.Add(new PlatformGame { PlatformId = 3, GameId = 1 });
        Context.PlatformGames.Add(new PlatformGame { PlatformId = 3, GameId = 2 });
        Context.PlatformGames.Add(new PlatformGame { PlatformId = 3, GameId = 5 });
        Context.PlatformGames.Add(new PlatformGame { PlatformId = 3, GameId = 6 });
    }
    
    private void AddStatValues()
    {
        Context.StatValues.Add(new StatValue { Id = 1, CharacterId = 1, Level = 1, StatId = 1, Value = 11, ContainedBonusNum = 2, ContainedBonusPercent = 0 });
        Context.StatValues.Add(new StatValue { Id = 2, CharacterId = 1, Level = 1, StatId = 2, Value = 22, ContainedBonusNum = 4, ContainedBonusPercent = 3 });
        Context.StatValues.Add(new StatValue { Id = 3, CharacterId = 1, Level = 1, StatId = 3, Value = 33, ContainedBonusNum = 8, ContainedBonusPercent = 8 });
        Context.StatValues.Add(new StatValue { Id = 4, CharacterId = 1, Level = 1, StatId = 4, Value = 44, ContainedBonusNum = 6, ContainedBonusPercent = 1 });
        Context.StatValues.Add(new StatValue { Id = 5, CharacterId = 2, Level = 5, StatId = 1, Value = 55, ContainedBonusNum = 0, ContainedBonusPercent = 2 });
        Context.StatValues.Add(new StatValue { Id = 6, CharacterId = 2, Level = 5, StatId = 2, Value = 66, ContainedBonusNum = 3, ContainedBonusPercent = 5 });
        Context.StatValues.Add(new StatValue { Id = 7, CharacterId = 2, Level = 5, StatId = 3, Value = 77, ContainedBonusNum = 5, ContainedBonusPercent = 0 });
        Context.StatValues.Add(new StatValue { Id = 8, CharacterId = 2, Level = 5, StatId = 5, Value = 88, ContainedBonusNum = 7, ContainedBonusPercent = 4 });
        Context.StatValues.Add(new StatValue { Id = 9, CharacterId = 3, Level = 9, StatId = 1, Value = 99, ContainedBonusNum = 11, ContainedBonusPercent = 2 });
        Context.StatValues.Add(new StatValue { Id = 10, CharacterId = 3, Level = 9, StatId = 2, Value = 100, ContainedBonusNum = 22, ContainedBonusPercent = 2 });
        Context.StatValues.Add(new StatValue { Id = 11, CharacterId = 3, Level = 9, StatId = 5, Value = 110, ContainedBonusNum = 33, ContainedBonusPercent = 4 });
        Context.StatValues.Add(new StatValue { Id = 12, CharacterId = 3, Level = 9, StatId = 6, Value = 120, ContainedBonusNum = 44, ContainedBonusPercent = 3 });
    }

    public void Dispose()
    {
        Context.Dispose();
        GC.SuppressFinalize(this);
    }
}