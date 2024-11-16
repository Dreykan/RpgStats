using System.ComponentModel.DataAnnotations;

namespace RpgStats.Dto.Tests;

public class GameForUpdateDtoTests
{
    [Fact]
    public void Name_IsRequired()
    {
        var dto = new GameForUpdateDto();
        var context = new ValidationContext(dto, null, null);
        var results = new List<ValidationResult>();

        var isValid = Validator.TryValidateObject(dto, context, results, true);

        Assert.False(isValid);
        Assert.Contains(results, r => r.ErrorMessage == "A name for the game is required.");
    }

    [Fact]
    public void Name_CannotExceedMaxLength()
    {
        var dto = new GameForUpdateDto { Name = new string('a', 101) };
        var context = new ValidationContext(dto, null, null);
        var results = new List<ValidationResult>();

        var isValid = Validator.TryValidateObject(dto, context, results, true);

        Assert.False(isValid);
        Assert.Contains(results, r => r.ErrorMessage == "The name for the game can't be longer than 100 characters.");
    }

    [Fact]
    public void Name_Valid()
    {
        var dto = new GameForUpdateDto { Name = "Valid Game Name" };
        var context = new ValidationContext(dto, null, null);
        var results = new List<ValidationResult>();

        var isValid = Validator.TryValidateObject(dto, context, results, true);

        Assert.True(isValid);
        Assert.Empty(results);
    }

    [Fact]
    public void Picture_CanBeNull()
    {
        var dto = new GameForUpdateDto { Name = "Valid Game Name", Picture = null };
        var context = new ValidationContext(dto, null, null);
        var results = new List<ValidationResult>();

        var isValid = Validator.TryValidateObject(dto, context, results, true);

        Assert.True(isValid);
        Assert.Empty(results);
    }

    [Fact]
    public void Picture_CanBeNonNull()
    {
        var dto = new GameForUpdateDto { Name = "Valid Game Name", Picture = new byte[] { 1, 2, 3 } };
        var context = new ValidationContext(dto, null, null);
        var results = new List<ValidationResult>();

        var isValid = Validator.TryValidateObject(dto, context, results, true);

        Assert.True(isValid);
        Assert.Empty(results);
    }
}