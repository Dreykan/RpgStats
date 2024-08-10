using RpgStats.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace RpgStats.Domain.Tests.Entities
{
    public class GameStatTests
    {
        [Fact]
        public void GameStat_Creation_Success()
        {
            // Arrange
            var gameStat = new GameStat
            {
                GameId = 1,
                StatId = 1
            };

            // Act
            var result = gameStat;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.GameId);
            Assert.Equal(1, result.StatId);
        }

        [Fact]
        public void GameStat_GameId_Required_Validation()
        {
            // Arrange
            var gameStat = new GameStat
            {
                StatId = 1
            };

            // Act
            var validationResults = ValidateModel(gameStat);

            // Assert
            Assert.NotEmpty(validationResults);
            Assert.Contains(validationResults, v => v.ErrorMessage.Contains("An entry for the column GameId is required."));
        }

        [Fact]
        public void GameStat_StatId_Required_Validation()
        {
            // Arrange
            var gameStat = new GameStat
            {
                GameId = 1
            };

            // Act
            var validationResults = ValidateModel(gameStat);

            // Assert
            Assert.NotEmpty(validationResults);
            Assert.Contains(validationResults, v => v.ErrorMessage.Contains("An entry for the column StatId is required."));
        }

        [Fact]
        public void GameStat_Relationships_Test()
        {
            // Arrange
            var game = new Game { Id = 1, Name = "Test Game" };
            var stat = new Stat { Id = 1, Name = "Test Stat" };
            var gameStat = new GameStat
            {
                GameId = 1,
                StatId = 1,
                Game = game,
                Stat = stat
            };

            // Act & Assert
            Assert.Equal(game, gameStat.Game);
            Assert.Equal(stat, gameStat.Stat);
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
