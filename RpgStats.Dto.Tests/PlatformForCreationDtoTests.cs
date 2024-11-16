using System.ComponentModel.DataAnnotations;

namespace RpgStats.Dto.Tests;

public class PlatformForCreationDtoTests
{
    [Fact]
    public void Name_IsRequired()
    {
        var dto = new PlatformForCreationDto();
        var context = new ValidationContext(dto, null, null);
        var results = new List<ValidationResult>();

        var isValid = Validator.TryValidateObject(dto, context, results, true);

        Assert.False(isValid);
        Assert.Contains(results, r => r.ErrorMessage == "A name for the platform is required.");
    }

    [Fact]
    public void Name_CannotExceedMaxLength()
    {
        var dto = new PlatformForCreationDto { Name = new string('a', 61) };
        var context = new ValidationContext(dto, null, null);
        var results = new List<ValidationResult>();

        var isValid = Validator.TryValidateObject(dto, context, results, true);

        Assert.False(isValid);
        Assert.Contains(results,
            r => r.ErrorMessage == "The name for the platform can't be longer than 60 characters.");
    }

    [Fact]
    public void Name_Valid()
    {
        var dto = new PlatformForCreationDto { Name = "Valid Platform Name" };
        var context = new ValidationContext(dto, null, null);
        var results = new List<ValidationResult>();

        var isValid = Validator.TryValidateObject(dto, context, results, true);

        Assert.True(isValid);
        Assert.Empty(results);
    }
}