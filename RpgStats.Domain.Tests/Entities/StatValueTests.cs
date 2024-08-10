using RpgStats.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace RpgStats.Domain.Tests.Entities
{
    public class StatValueTests
    {
        [Fact]
        public void StatValue_Creation_Success()
        {
            // Arrange
            var statValue = new StatValue
            {
                Level = 1,
                CharacterId = 1,
                StatId = 1,
                Value = 10,
                ContainedBonusNum = 5,
                ContainedBonusPercent = 10
            };

            // Act
            var result = statValue;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Level);
            Assert.Equal(1, result.CharacterId);
            Assert.Equal(1, result.StatId);
            Assert.Equal(10, result.Value);
            Assert.Equal(5, result.ContainedBonusNum);
            Assert.Equal(10, result.ContainedBonusPercent);
        }

        [Fact]
        public void StatValue_Level_Required_Validation()
        {
            // Arrange
            var statValue = new StatValue
            {
                CharacterId = 1,
                StatId = 1,
                Value = 10,
                ContainedBonusNum = 5,
                ContainedBonusPercent = 10
            };

            // Act
            var validationResults = ValidateModel(statValue);

            // Assert
            Assert.NotEmpty(validationResults);
            Assert.Contains(validationResults, v => v.ErrorMessage.Contains("A Level entry is required."));
        }

        [Fact]
        public void StatValue_CharacterId_Required_Validation()
        {
            // Arrange
            var statValue = new StatValue
            {
                Level = 1,
                StatId = 1,
                Value = 10,
                ContainedBonusNum = 5,
                ContainedBonusPercent = 10
            };

            // Act
            var validationResults = ValidateModel(statValue);

            // Assert
            Assert.NotEmpty(validationResults);
            Assert.Contains(validationResults, v => v.ErrorMessage.Contains("An Character-Entry is required."));
        }

        [Fact]
        public void StatValue_StatId_Required_Validation()
        {
            // Arrange
            var statValue = new StatValue
            {
                Level = 1,
                CharacterId = 1,
                Value = 10,
                ContainedBonusNum = 5,
                ContainedBonusPercent = 10
            };

            // Act
            var validationResults = ValidateModel(statValue);

            // Assert
            Assert.NotEmpty(validationResults);
            Assert.Contains(validationResults, v => v.ErrorMessage.Contains("A Stat-Entry is required."));
        }

        [Fact]
        public void StatValue_Value_Required_Validation()
        {
            // Arrange
            var statValue = new StatValue
            {
                Level = 1,
                CharacterId = 1,
                StatId = 1,
                ContainedBonusNum = 5,
                ContainedBonusPercent = 10
            };

            // Act
            var validationResults = ValidateModel(statValue);

            // Assert
            Assert.NotEmpty(validationResults);
            Assert.Contains(validationResults, v => v.ErrorMessage.Contains("A value for the stat entry is required."));
        }

        [Fact]
        public void StatValue_ContainedBonusNum_Required_Validation()
        {
            // Arrange
            var statValue = new StatValue
            {
                Level = 1,
                CharacterId = 1,
                StatId = 1,
                Value = 10,
                ContainedBonusPercent = 10
            };

            // Act
            var validationResults = ValidateModel(statValue);

            // Assert
            Assert.NotEmpty(validationResults);
            Assert.Contains(validationResults, v => v.ErrorMessage.Contains("A bonus contained herein in numbers for this entry is required."));
        }

        [Fact]
        public void StatValue_ContainedBonusPercent_Required_Validation()
        {
            // Arrange
            var statValue = new StatValue
            {
                Level = 1,
                CharacterId = 1,
                StatId = 1,
                Value = 10,
                ContainedBonusNum = 5
            };

            // Act
            var validationResults = ValidateModel(statValue);

            // Assert
            Assert.NotEmpty(validationResults);
            Assert.Contains(validationResults, v => v.ErrorMessage.Contains("A bonus contained herein in percent for this entry is required."));
        }

        private List<ValidationResult> ValidateModel(object model)
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, validationContext, validationResults, true);
            return validationResults;
        }
    }
}
