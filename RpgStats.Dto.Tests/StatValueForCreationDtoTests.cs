using System.ComponentModel.DataAnnotations;

namespace RpgStats.Dto.Tests;

public class StatValueForCreationDtoTests
{
    [Fact]
    public void StatValueForCreationDto_ValidData_ShouldPassValidation()
    {
        var dto = new StatValueForCreationDto
        {
            Level = 1,
            Value = 10,
            ContainedBonusNum = 5,
            ContainedBonusPercent = 20
        };

        var validationResults = new List<ValidationResult>();
        var validationContext = new ValidationContext(dto);
        var isValid = Validator.TryValidateObject(dto, validationContext, validationResults, true);

        Assert.True(isValid);
    }

    [Fact]
    public void StatValueForCreationDto_MissingLevel_ShouldFailValidation()
    {
        var dto = new StatValueForCreationDto
        {
            Value = 10,
            ContainedBonusNum = 5,
            ContainedBonusPercent = 20
        };

        var validationResults = new List<ValidationResult>();
        var validationContext = new ValidationContext(dto);
        var isValid = Validator.TryValidateObject(dto, validationContext, validationResults, true);

        Assert.False(isValid);
        Assert.Contains(validationResults, v => v.ErrorMessage == "A Level entry is required.");
    }

    [Fact]
    public void StatValueForCreationDto_MissingValue_ShouldFailValidation()
    {
        var dto = new StatValueForCreationDto
        {
            Level = 1,
            ContainedBonusNum = 5,
            ContainedBonusPercent = 2,
        };

        var validationResults = new List<ValidationResult>();
        var validationContext = new ValidationContext(dto);
        var isValid = Validator.TryValidateObject(dto, validationContext, validationResults, true);

        Assert.False(isValid);
        Assert.Contains(validationResults, v => v.ErrorMessage == "A value for the stat entry is required.");
    }

    [Fact]
    public void StatValueForCreationDto_MissingContainedBonusNum_ShouldFailValidation()
    {
        var dto = new StatValueForCreationDto
        {
            Level = 1,
            Value = 10,
            ContainedBonusPercent = 20
        };

        var validationResults = new List<ValidationResult>();
        var validationContext = new ValidationContext(dto);
        var isValid = Validator.TryValidateObject(dto, validationContext, validationResults, true);

        Assert.False(isValid);
        Assert.Contains(validationResults, v => v.ErrorMessage == "A bonus contained herein in numbers for this entry is required.");
    }

    [Fact]
    public void StatValueForCreationDto_MissingContainedBonusPercent_ShouldFailValidation()
    {
        var dto = new StatValueForCreationDto
        {
            Level = 1,
            Value = 10,
            ContainedBonusNum = 5
        };

        var validationResults = new List<ValidationResult>();
        var validationContext = new ValidationContext(dto);
        var isValid = Validator.TryValidateObject(dto, validationContext, validationResults, true);

        Assert.False(isValid);
        Assert.Contains(validationResults, v => v.ErrorMessage == "A bonus contained herein in percent for this entry is required.");
    }
}