using System.ComponentModel.DataAnnotations;

namespace RpgStats.Dto.Tests;

public class PlatformForUpdateDtoTests
{
    [Fact]
    public void Name_IsRequired()
    {
        var dto = new PlatformForUpdateDto();
        var validationResults = new List<ValidationResult>();
        var validationContext = new ValidationContext(dto);

        var isValid = Validator.TryValidateObject(dto, validationContext, validationResults, true);

        Assert.False(isValid);
        Assert.Contains(validationResults, v => v.ErrorMessage == "A name for the platform is required.");
    }

    [Fact]
    public void Name_CannotBeLongerThan60Characters()
    {
        var dto = new PlatformForUpdateDto { Name = new string('a', 61) };
        var validationResults = new List<ValidationResult>();
        var validationContext = new ValidationContext(dto);

        var isValid = Validator.TryValidateObject(dto, validationContext, validationResults, true);

        Assert.False(isValid);
        Assert.Contains(validationResults, v => v.ErrorMessage == "The name for the platform can't be longer than 60 characters.");
    }

    [Fact]
    public void Name_ValidName_PassesValidation()
    {
        var dto = new PlatformForUpdateDto { Name = "Valid Platform Name" };
        var validationResults = new List<ValidationResult>();
        var validationContext = new ValidationContext(dto);

        var isValid = Validator.TryValidateObject(dto, validationContext, validationResults, true);

        Assert.True(isValid);
    }
}