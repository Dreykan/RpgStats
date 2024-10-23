using System.ComponentModel.DataAnnotations;

namespace RpgStats.Dto.Tests;

public class StatValueForUpdateDtoTests
{
    [Fact]
    public void StatValueForUpdateDto_ValidData_ShouldPassValidation()
    {
        var dto = new StatValueForUpdateDto
        {
            Level = 1,
            Value = 10,
            ContainedBonusNum = 5,
            ContainedBonusPercent = 20
        };

        var validationResults = new List<ValidationResult>();
        var validationContext = new ValidationContext(dto);

        bool isValid = Validator.TryValidateObject(dto, validationContext, validationResults, true);

        Assert.True(isValid);
    }

    [Fact]
    public void StatValueForUpdateDto_MissingLevel_ShouldFailValidation()
    {
        var dto = new StatValueForUpdateDto
        {
            Value = 10,
            ContainedBonusNum = 5,
            ContainedBonusPercent = 20
        };

        var validationResults = new List<ValidationResult>();
        var validationContext = new ValidationContext(dto);

        bool isValid = Validator.TryValidateObject(dto, validationContext, validationResults, true);

        Assert.False(isValid);
        Assert.Contains(validationResults, v => v.ErrorMessage == "A Level entry is required.");
    }

    [Fact]
    public void StatValueForUpdateDto_MissingValue_ShouldFailValidation()
    {
        var dto = new StatValueForUpdateDto
        {
            Level = 1,
            ContainedBonusNum = 5,
            ContainedBonusPercent = 20
        };

        var validationResults = new List<ValidationResult>();
        var validationContext = new ValidationContext(dto);

        bool isValid = Validator.TryValidateObject(dto, validationContext, validationResults, true);

        Assert.False(isValid);
        Assert.Contains(validationResults, v => v.ErrorMessage == "A value for the stat entry is required.");
    }

    [Fact]
    public void StatValueForUpdateDto_MissingContainedBonusNum_ShouldFailValidation()
    {
        var dto = new StatValueForUpdateDto
        {
            Level = 1,
            Value = 10,
            ContainedBonusPercent = 20
        };

        var validationResults = new List<ValidationResult>();
        var validationContext = new ValidationContext(dto);

        bool isValid = Validator.TryValidateObject(dto, validationContext, validationResults, true);

        Assert.False(isValid);
        Assert.Contains(validationResults, v => v.ErrorMessage == "A bonus contained herein in numbers for this entry is required.");
    }

    [Fact]
    public void StatValueForUpdateDto_MissingContainedBonusPercent_ShouldFailValidation()
    {
        var dto = new StatValueForUpdateDto
        {
            Level = 1,
            Value = 10,
            ContainedBonusNum = 5
        };

        var validationResults = new List<ValidationResult>();
        var validationContext = new ValidationContext(dto);

        bool isValid = Validator.TryValidateObject(dto, validationContext, validationResults, true);

        Assert.False(isValid);
        Assert.Contains(validationResults, v => v.ErrorMessage == "A bonus contained herein in percent for this entry is required.");
    }
}