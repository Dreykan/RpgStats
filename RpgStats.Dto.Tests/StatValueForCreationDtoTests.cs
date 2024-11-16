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
}