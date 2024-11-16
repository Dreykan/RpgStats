using RpgStats.Domain.Entities;

// ReSharper disable UseObjectOrCollectionInitializer

namespace RpgStats.Domain.Tests.Entities;

public class PlatformTests
{
    [Fact]
    public void Platform_Id_ShouldBeSetAndRetrievedCorrectly()
    {
        var platform = new Platform();

        platform.Id = 12345;

        Assert.Equal(12345, platform.Id);
    }

    [Fact]
    public void Platform_Name_ShouldBeSetAndRetrievedCorrectly()
    {
        var platform = new Platform();

        platform.Name = "TestPlatform";

        Assert.Equal("TestPlatform", platform.Name);
    }

    [Fact]
    public void Platform_PlatformGames_ShouldBeSetAndRetrievedCorrectly()
    {
        var platform = new Platform();

        platform.PlatformGames = new List<PlatformGame> { new() { PlatformId = 12345 } };

        Assert.Equal(12345, platform.PlatformGames.First().PlatformId);
    }

    [Fact]
    public void Platform_Name_RequiredValidation()
    {
        var platform = new Platform();

        var validationResults = TestHelper.ValidateModel(platform);

        Assert.NotEmpty(validationResults);
        Assert.Contains(validationResults,
            v => v.ErrorMessage != null && v.ErrorMessage.Contains("A name for the platform is required."));
    }

    [Fact]
    public void Platform_Name_LengthValidation()
    {
        var platform = new Platform
        {
            Name = new string('a', 61)
        };

        var validationResults = TestHelper.ValidateModel(platform);

        Assert.NotEmpty(validationResults);
        Assert.Contains(validationResults,
            v => v.ErrorMessage != null &&
                 v.ErrorMessage.Contains("The name for the platform can't be longer than 60 characters."));
    }
}