using System.ComponentModel.DataAnnotations;

namespace RpgStats.Dto.Tests;

public class GameStatForUpdateDtoTests
{
    [Fact]
    public void GameStatForUpdateDto_Initialized_HasCorrectValues()
    {
        var gameStatForUpdateDto = new GameStatForUpdateDto
        {
            SortIndex = 1,
            CustomStatName = "CustomStatName",
            CustomStatShortName = "ShortName",
            GameId = 1,
            StatId = 1
        };

        var result = gameStatForUpdateDto;

        Assert.NotNull(result);
        Assert.Equal(1, result.SortIndex);
        Assert.Equal("CustomStatName", result.CustomStatName);
        Assert.Equal("ShortName", result.CustomStatShortName);
        Assert.Equal(1, result.GameId);
        Assert.Equal(1, result.StatId);
    }

    [Fact]
    public void GameStatForUpdateDto_Initialized_HasCorrectValues_WithDefaultValues()
    {
        var gameStatForUpdateDto = new GameStatForUpdateDto();

        var result = gameStatForUpdateDto;

        Assert.NotNull(result);
        Assert.Equal(0, result.SortIndex);
        Assert.Equal(string.Empty, result.CustomStatName);
        Assert.Equal(string.Empty, result.CustomStatShortName);
        Assert.Equal(0, result.GameId);
        Assert.Equal(0, result.StatId);
    }

    [Fact]
    public void GameStatForUpdateDto_NameTooLong_IsInvalid()
    {
        var gameStatForUpdateDto = new GameStatForUpdateDto
        {
            CustomStatName = new string('a', 51)
        };

        var context = new ValidationContext(gameStatForUpdateDto, null, null);
        var results = new List<ValidationResult>();

        var isValid = Validator.TryValidateObject(gameStatForUpdateDto, context, results, true);

        Assert.False(isValid);
        Assert.Contains(results, r => r.ErrorMessage == "The name cannot be longer than 50 characters.");
    }

    [Fact]
    public void GameStatForUpdateDto_ShortNameTooLong_IsInvalid()
    {
        var gameStatForUpdateDto = new GameStatForUpdateDto
        {
            CustomStatShortName = new string('a', 9)
        };

        var context = new ValidationContext(gameStatForUpdateDto, null, null);
        var results = new List<ValidationResult>();

        var isValid = Validator.TryValidateObject(gameStatForUpdateDto, context, results, true);

        Assert.False(isValid);
        Assert.Contains(results, r => r.ErrorMessage == "The shortname for the stat can't be longer than 8 characters.");
    }
}