using System.ComponentModel.DataAnnotations;

namespace RpgStats.Dto.Tests;

public class GameForCreationDtoTests
{
    [Fact]
    public void Name_IsRequired()
    {
        var dto = new GameForCreationDto();
        var context = new ValidationContext(dto, null, null);
        var results = new List<ValidationResult>();

        var isValid = Validator.TryValidateObject(dto, context, results, true);

        Assert.False(isValid);
        Assert.Contains(results, r => r.ErrorMessage == "A name for the game is required.");
    }

    [Fact]
    public void Name_CannotExceedMaxLength()
    {
        var dto = new GameForCreationDto { Name = new string('a', 101) };
        var context = new ValidationContext(dto, null, null);
        var results = new List<ValidationResult>();

        var isValid = Validator.TryValidateObject(dto, context, results, true);

        Assert.False(isValid);
        Assert.Contains(results, r => r.ErrorMessage == "The name for the game can't be longer than 100 characters.");
    }

    [Fact]
    public void Name_WithinValidLength()
    {
        var dto = new GameForCreationDto { Name = new string('a', 100) };
        var context = new ValidationContext(dto, null, null);
        var results = new List<ValidationResult>();

        var isValid = Validator.TryValidateObject(dto, context, results, true);

        Assert.True(isValid);
        Assert.Empty(results);
    }

    [Fact]
    public void Picture_CanBeNull()
    {
        var dto = new GameForCreationDto { Name = "Valid Name", Picture = null };
        var context = new ValidationContext(dto, null, null);
        var results = new List<ValidationResult>();

        var isValid = Validator.TryValidateObject(dto, context, results, true);

        Assert.True(isValid);
        Assert.Empty(results);
    }

    [Fact]
    public void Picture_CanHaveValue()
    {
        var dto = new GameForCreationDto { Name = "Valid Name", Picture = new byte[] { 1, 2, 3 } };
        var context = new ValidationContext(dto, null, null);
        var results = new List<ValidationResult>();

        var isValid = Validator.TryValidateObject(dto, context, results, true);

        Assert.True(isValid);
        Assert.Empty(results);
    }
}