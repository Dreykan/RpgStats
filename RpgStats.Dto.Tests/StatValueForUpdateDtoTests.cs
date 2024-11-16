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
}