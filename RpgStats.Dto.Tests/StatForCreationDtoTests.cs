using System.ComponentModel.DataAnnotations;

namespace RpgStats.Dto.Tests;

public class StatForCreationDtoTests
{
    [Fact]
    public void Name_IsRequired()
    {
        var dto = new StatForCreationDto { Name = null };
        var validationResults = ValidateModel(dto);
        Assert.Contains(validationResults, v => v.ErrorMessage == "A name for the stat is required.");
    }

    [Fact]
    public void Name_CannotExceed50Characters()
    {
        var dto = new StatForCreationDto { Name = new string('a', 51) };
        var validationResults = ValidateModel(dto);
        Assert.Contains(validationResults, v => v.ErrorMessage == "The name for the stat can't be longer than 50 characters.");
    }

    [Fact]
    public void Name_WithinValidLength()
    {
        var dto = new StatForCreationDto { Name = new string('a', 50) };
        var validationResults = ValidateModel(dto);
        Assert.DoesNotContain(validationResults, v => v.ErrorMessage == "The name for the stat can't be longer than 50 characters.");
    }

    [Fact]
    public void ShortName_CannotExceed8Characters()
    {
        var dto = new StatForCreationDto { ShortName = new string('a', 9) };
        var validationResults = ValidateModel(dto);
        Assert.Contains(validationResults, v => v.ErrorMessage == "The shortname for the stat can't be longer than 8 characters.");
    }

    [Fact]
    public void ShortName_WithinValidLength()
    {
        var dto = new StatForCreationDto { ShortName = new string('a', 8) };
        var validationResults = ValidateModel(dto);
        Assert.DoesNotContain(validationResults, v => v.ErrorMessage == "The shortname for the stat can't be longer than 8 characters.");
    }

    private IList<ValidationResult> ValidateModel(object model)
    {
        var validationResults = new List<ValidationResult>();
        var validationContext = new ValidationContext(model, null, null);
        Validator.TryValidateObject(model, validationContext, validationResults, true);
        return validationResults;
    }
}