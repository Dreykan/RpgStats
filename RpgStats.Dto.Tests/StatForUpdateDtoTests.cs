using System.ComponentModel.DataAnnotations;

namespace RpgStats.Dto.Tests;

public class StatForUpdateDtoTests
{
    [Fact]
    public void Name_IsRequired()
    {
        var dto = new StatForUpdateDto();
        var validationResults = new List<ValidationResult>();
        var context = new ValidationContext(dto, null, null);
        var isValid = Validator.TryValidateObject(dto, context, validationResults, true);

        Assert.False(isValid);
        Assert.Contains(validationResults, v => v.ErrorMessage == "A name for the stat is required.");
    }

    [Fact]
    public void Name_CannotExceed50Characters()
    {
        var dto = new StatForUpdateDto { Name = new string('a', 51) };
        var validationResults = new List<ValidationResult>();
        var context = new ValidationContext(dto, null, null);
        var isValid = Validator.TryValidateObject(dto, context, validationResults, true);

        Assert.False(isValid);
        Assert.Contains(validationResults,
            v => v.ErrorMessage == "The name for the stat can't be longer than 50 characters.");
    }

    [Fact]
    public void Name_WithinValidLength()
    {
        var dto = new StatForUpdateDto { Name = new string('a', 50) };
        var validationResults = new List<ValidationResult>();
        var context = new ValidationContext(dto, null, null);
        var isValid = Validator.TryValidateObject(dto, context, validationResults, true);

        Assert.True(isValid);
    }

    [Fact]
    public void ShortName_CannotExceed8Characters()
    {
        var dto = new StatForUpdateDto { ShortName = new string('a', 9) };
        var validationResults = new List<ValidationResult>();
        var context = new ValidationContext(dto, null, null);
        var isValid = Validator.TryValidateObject(dto, context, validationResults, true);

        Assert.False(isValid);
        Assert.Contains(validationResults,
            v => v.ErrorMessage == "The shortname for the stat can't be longer than 8 characters.");
    }

    [Fact]
    public void ShortName_WithinValidLength()
    {
        var dto = new StatForUpdateDto { Name = new string('b', 25), ShortName = new string('a', 8) };
        var validationResults = new List<ValidationResult>();
        var context = new ValidationContext(dto, null, null);
        var isValid = Validator.TryValidateObject(dto, context, validationResults, true);

        Assert.True(isValid);
    }
}