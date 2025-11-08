using System.ComponentModel.DataAnnotations;

namespace RpgStats.Dto.Tests;

public class CharacterForUpdateDtoTests
{
    [Fact]
    public void Name_CannotExceedMaxLength()
    {
        var dto = new CharacterForUpdateDto { Name = new string('a', 61) };
        var context = new ValidationContext(dto, null, null);
        var results = new List<ValidationResult>();

        var isValid = Validator.TryValidateObject(dto, context, results, true);

        Assert.False(isValid);
        Assert.Contains(results, r => r.ErrorMessage == "The name can't be longer than 60 characters.");
    }

    [Fact]
    public void Name_ValidLength()
    {
        var dto = new CharacterForUpdateDto { Name = new string('a', 60) };
        var context = new ValidationContext(dto, null, null);
        var results = new List<ValidationResult>();

        var isValid = Validator.TryValidateObject(dto, context, results, true);

        Assert.True(isValid);
        Assert.Empty(results);
    }

    [Fact]
    public void Picture_CanBeNull()
    {
        var dto = new CharacterForUpdateDto { Name = "ValidName", Picture = null };
        var context = new ValidationContext(dto, null, null);
        var results = new List<ValidationResult>();

        var isValid = Validator.TryValidateObject(dto, context, results, true);

        Assert.True(isValid);
        Assert.Empty(results);
    }

    [Fact]
    public void Picture_CanHaveValue()
    {
        var dto = new CharacterForUpdateDto { Name = "ValidName", Picture = new byte[] { 1, 2, 3 } };
        var context = new ValidationContext(dto, null, null);
        var results = new List<ValidationResult>();

        var isValid = Validator.TryValidateObject(dto, context, results, true);

        Assert.True(isValid);
        Assert.Empty(results);
    }

    [Fact]
    public void Note_CannotExceedMaxLength()
    {
        var dto = new CharacterForUpdateDto { Name = "ValidName", Note = new string('a', 2001) };
        var context = new ValidationContext(dto, null, null);
        var results = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(dto, context, results, true);

        Assert.False(isValid);
        Assert.Contains(results, r => r.ErrorMessage == "The note can't be longer than 2000 characters.");
    }
}