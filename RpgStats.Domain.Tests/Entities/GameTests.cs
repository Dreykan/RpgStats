using RpgStats.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace RpgStats.Domain.Tests.Entities
{
    public class GameTests
    {
        [Fact]
        public void Game_Creation_Success()
        {
            // Arrange
            var game = new Game
            {
                Name = "Test Game"
            };

            // Act
            var result = game;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Test Game", result.Name);
        }

        [Fact]
        public void Game_Name_Required_Validation()
        {
            // Arrange
            var game = new Game();

            // Act
            var validationResults = ValidateModel(game);

            // Assert
            Assert.NotEmpty(validationResults);
            Assert.Contains(validationResults, v => v.ErrorMessage.Contains("A name for the game is required."));
        }

        [Fact]
        public void Game_Name_Length_Validation()
        {
            // Arrange
            var game = new Game
            {
                Name = new string('a', 101)
            };

            // Act
            var validationResults = ValidateModel(game);

            // Assert
            Assert.NotEmpty(validationResults);
            Assert.Contains(validationResults, v => v.ErrorMessage.Contains("The name for the game can't be longer than 100 characters."));
        }

        [Fact]
        public void Game_Relationships_Test()
        {
            // Arrange
            var platformGame = new PlatformGame { GameId = 1 };
            var gameStat = new GameStat { GameId = 1 };
            var character = new Character { GameId = 1 };
            var game = new Game
            {
                Name = "Test Game",
                PlatformGames = new List<PlatformGame> { platformGame },
                GameStats = new List<GameStat> { gameStat },
                Characters = new List<Character> { character }
            };

            // Act & Assert
            Assert.Contains(platformGame, game.PlatformGames);
            Assert.Contains(gameStat, game.GameStats);
            Assert.Contains(character, game.Characters);
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
