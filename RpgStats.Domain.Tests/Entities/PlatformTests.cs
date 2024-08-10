using System.ComponentModel.DataAnnotations;
using RpgStats.Domain.Entities;

namespace RpgStats.Domain.Tests.Entities
{
    public class PlatformTests
    {
        [Fact]
        public void Platform_Id_Should_Be_Set_Correctly()
        {
            // Arrange
            var platform = new Platform { Id = 1 };

            // Act
            var result = platform.Id;

            // Assert
            Assert.Equal(1, result);
        }

        [Fact]
        public void Platform_Name_Should_Be_Required()
        {
            // Arrange
            var platform = new Platform { Name = null };
            var validationContext = new ValidationContext(platform);
            var validationResults = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(platform, validationContext, validationResults, true);

            // Assert
            Assert.False(isValid);
            Assert.Contains(validationResults, v => v.ErrorMessage.Contains("A name for the platform is required."));
        }

        [Fact]
        public void Platform_Name_Should_Not_Exceed_Max_Length()
        {
            // Arrange
            var platform = new Platform { Name = new string('a', 61) };
            var validationContext = new ValidationContext(platform);
            var validationResults = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(platform, validationContext, validationResults, true);

            // Assert
            Assert.False(isValid);
            Assert.Contains(validationResults, v => v.ErrorMessage.Contains("The name for the platform can't be longer than 60 characters."));
        }

        [Fact]
        public void PlatformGames_Should_Be_Initialized_Correctly()
        {
            // Arrange
            var platform = new Platform { PlatformGames = new List<PlatformGame>() };

            // Act
            var result = platform.PlatformGames;

            // Assert
            Assert.NotNull(result);
        }
    }
}
