using RpgStats.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace RpgStats.Domain.Tests.Entities
{
    public class CharacterTests
    {
        [Fact]
        public void Character_Creation_Success()
        {
            // Arrange
            var character = new Character
            {
                Name = "Test Character",
                GameId = 1
            };

            // Act
            var result = character;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Test Character", result.Name);
            Assert.Equal(1, result.GameId);
        }

        [Fact]
        public void Character_Name_Required_Validation()
        {
            // Arrange
            var character = new Character
            {
                GameId = 1
            };

            // Act
            var validationResults = ValidateModel(character);

            // Assert
            Assert.NotEmpty(validationResults);
            Assert.Contains(validationResults, v => v.ErrorMessage.Contains("A name for a character is required."));
        }

        [Fact]
        public void Character_Name_Length_Validation()
        {
            // Arrange
            var character = new Character
            {
                Name = new string('a', 61),
                GameId = 1
            };

            // Act
            var validationResults = ValidateModel(character);

            // Assert
            Assert.NotEmpty(validationResults);
            Assert.Contains(validationResults, v => v.ErrorMessage.Contains("The Name can't be longer than 60 characters."));
        }

        [Fact]
        public void Character_GameId_Required_Validation()
        {
            // Arrange
            var character = new Character
            {
                Name = "Hero"
            };

            // Act
            var validationResults = ValidateModel(character);

            // Assert
            Assert.NotEmpty(validationResults);
            Assert.Contains(validationResults, v => v.ErrorMessage.Contains("An entry in the column GameId is required."));
        }

        [Fact]
        public void Character_Relationships_Test()
        {
            // Arrange
            var game = new Game { Id = 1, Name = "Test Game" };
            var statValue = new StatValue { Id = 1, CharacterId = 1, Value = 10 };
            var character = new Character
            {
                Name = "Hero",
                GameId = 1,
                Game = game,
                StatValues = new List<StatValue> { statValue }
            };

            // Act & Assert
            Assert.Equal(game, character.Game);
            Assert.Contains(statValue, character.StatValues);
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
