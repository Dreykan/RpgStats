﻿using RpgStats.Domain.Entities;

// ReSharper disable UseObjectOrCollectionInitializer

namespace RpgStats.Domain.Tests.Entities;

public class PlatformGameTests
{
    [Fact]
    public void PlatformGame_Id_ShouldBeSetAndRetrievedCorrectly()
    {
        var platformGame = new PlatformGame();

        platformGame.Id = 12345;

        Assert.Equal(12345, platformGame.Id);
    }

    [Fact]
    public void PlatformGame_PlatformId_ShouldBeSetAndRetrievedCorrectly()
    {
        var platformGame = new PlatformGame();

        platformGame.PlatformId = 12345;

        Assert.Equal(12345, platformGame.PlatformId);
    }

    [Fact]
    public void PlatformGame_Platform_ShouldBeSetAndRetrievedCorrectly()
    {
        var platformGame = new PlatformGame();

        platformGame.Platform = new Platform { Id = 12345, Name = "TestPlatform" };

        Assert.Equal(12345, platformGame.Platform.Id);
        Assert.Equal("TestPlatform", platformGame.Platform.Name);
    }

    [Fact]
    public void PlatformGame_GameId_ShouldBeSetAndRetrievedCorrectly()
    {
        var platformGame = new PlatformGame();

        platformGame.GameId = 12345;

        Assert.Equal(12345, platformGame.GameId);
    }

    [Fact]
    public void PlatformGame_Game_ShouldBeSetAndRetrievedCorrectly()
    {
        var platformGame = new PlatformGame();

        platformGame.Game = new Game { Id = 12345, Name = "TestGame" };

        Assert.Equal(12345, platformGame.Game.Id);
        Assert.Equal("TestGame", platformGame.Game.Name);
    }
}