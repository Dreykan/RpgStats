using System.ComponentModel.DataAnnotations;
using RpgStats.Domain.Entities;

namespace RpgStats.Domain.Tests.Entities
{
    public class PlatformGameTests
    {
        [Fact]
        public void PlatformGame_Should_Have_Default_Values()
        {
            var platformGame = new PlatformGame();
            Assert.Equal(0, platformGame.Id);
            Assert.Null(platformGame.PlatformId);
            Assert.Null(platformGame.Platform);
            Assert.Null(platformGame.GameId);
            Assert.Null(platformGame.Game);
        }

        [Fact]
        public void PlatformGame_Should_Set_And_Get_Properties()
        {
            var platformGame = new PlatformGame
            {
                Id = 1,
                PlatformId = 2,
                GameId = 3
            };

            Assert.Equal(1, platformGame.Id);
            Assert.Equal(2, platformGame.PlatformId);
            Assert.Equal(3, platformGame.GameId);
        }

        [Fact]
        public void PlatformGame_Should_Throw_Validation_Exception_For_Missing_PlatformId()
        {
            var platformGame = new PlatformGame { GameId = 1 };
            var context = new ValidationContext(platformGame, null, null);
            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(platformGame, context, results, true);

            Assert.False(isValid);
            Assert.Contains(results, v => v.ErrorMessage == "An entry for column PlatformId is required.");
        }

        [Fact]
        public void PlatformGame_Should_Throw_Validation_Exception_For_Missing_GameId()
        {
            var platformGame = new PlatformGame { PlatformId = 1 };
            var context = new ValidationContext(platformGame, null, null);
            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(platformGame, context, results, true);

            Assert.False(isValid);
            Assert.Contains(results, v => v.ErrorMessage == "An entry for the column GameId is required.");
        }
    }
}
