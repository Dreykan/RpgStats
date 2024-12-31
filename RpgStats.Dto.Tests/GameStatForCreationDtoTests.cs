using System.ComponentModel.DataAnnotations;

namespace RpgStats.Dto.Tests;

public class GameStatForCreationDtoTests
{
    [Fact]
    public void GameStatForCreationDto_Initialized_HasCorrectValues()
    {
        var gameStatForCreationDto = new GameStatForCreationDto
        {
            SortIndex = 1,
            CustomStatName = "CustomStatName",
            CustomStatShortName = "ShortName",
            GameId = 1,
            StatId = 1
        };

        var result = gameStatForCreationDto;

        Assert.NotNull(result);
        Assert.Equal(1, result.SortIndex);
        Assert.Equal("CustomStatName", result.CustomStatName);
        Assert.Equal("ShortName", result.CustomStatShortName);
        Assert.Equal(1, result.GameId);
        Assert.Equal(1, result.StatId);
    }

    [Fact]
    public void GameStatForCreationDto_Initialized_HasCorrectValues_WithDefaultValues()
    {
        var gameStatForCreationDto = new GameStatForCreationDto();

        var result = gameStatForCreationDto;

        Assert.NotNull(result);
        Assert.Equal(0, result.SortIndex);
        Assert.Equal(string.Empty, result.CustomStatName);
        Assert.Equal(string.Empty, result.CustomStatShortName);
        Assert.Equal(0, result.GameId);
        Assert.Equal(0, result.StatId);
    }

    [Fact]
    public void GameStatForCreationDto_NameTooLong_IsInvalid()
    {
        var gameStatForCreationDto = new GameStatForCreationDto
        {
            CustomStatName = new string('a', 51)
        };

        var context = new ValidationContext(gameStatForCreationDto, null, null);
        var results = new List<ValidationResult>();

        var isValid = Validator.TryValidateObject(gameStatForCreationDto, context, results, true);

        Assert.False(isValid);
        Assert.Contains(results, r => r.ErrorMessage == "The name cannot be longer than 50 characters.");
    }

    [Fact]
    public void GameStatForCreationDto_ShortNameTooLong_IsInvalid()
    {
        var gameStatForCreationDto = new GameStatForCreationDto
        {
            CustomStatShortName = new string('a', 9)
        };

        var context = new ValidationContext(gameStatForCreationDto, null, null);
        var results = new List<ValidationResult>();

        var isValid = Validator.TryValidateObject(gameStatForCreationDto, context, results, true);

        Assert.False(isValid);
        Assert.Contains(results, r => r.ErrorMessage == "The shortname for the stat can't be longer than 8 characters.");
    }
}