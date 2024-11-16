using System.ComponentModel.DataAnnotations;

namespace RpgStats.Dto.Tests;

public class CharacterForCreationDtoTests
{
    [Fact]
    public void Name_IsRequired()
    {
        var character = new CharacterForCreationDto();
        var validationResults = new List<ValidationResult>();
        var context = new ValidationContext(character);

        var isValid = Validator.TryValidateObject(character, context, validationResults, true);

        Assert.False(isValid);
        Assert.Contains(validationResults, v => v.ErrorMessage == "A name for a character is required.");
    }

    [Fact]
    public void Name_CannotBeLongerThan60Characters()
    {
        var character = new CharacterForCreationDto { Name = new string('a', 61) };
        var validationResults = new List<ValidationResult>();
        var context = new ValidationContext(character);

        var isValid = Validator.TryValidateObject(character, context, validationResults, true);

        Assert.False(isValid);
        Assert.Contains(validationResults, v => v.ErrorMessage == "The name can't be longer than 60 characters.");
    }

    [Fact]
    public void Name_ValidName_PassesValidation()
    {
        var character = new CharacterForCreationDto { Name = "Valid Name" };
        var validationResults = new List<ValidationResult>();
        var context = new ValidationContext(character);

        var isValid = Validator.TryValidateObject(character, context, validationResults, true);

        Assert.True(isValid);
    }

    [Fact]
    public void Picture_CanBeNull()
    {
        var character = new CharacterForCreationDto { Name = "Valid Name", Picture = null };
        var validationResults = new List<ValidationResult>();
        var context = new ValidationContext(character);

        var isValid = Validator.TryValidateObject(character, context, validationResults, true);

        Assert.True(isValid);
    }

    [Fact]
    public void Picture_CanBeNonNull()
    {
        var character = new CharacterForCreationDto { Name = "Valid Name", Picture = new byte[] { 1, 2, 3 } };
        var validationResults = new List<ValidationResult>();
        var context = new ValidationContext(character);

        var isValid = Validator.TryValidateObject(character, context, validationResults, true);

        Assert.True(isValid);
    }
}