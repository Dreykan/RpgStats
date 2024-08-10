using RpgStats.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace RpgStats.Domain.Tests.Entities
{
    public class StatTests
    {
        [Fact]
        public void Stat_Should_Have_Default_Values()
        {
            var stat = new Stat();

            Assert.Equal(0, stat.Id);
            Assert.Null(stat.Name);
            Assert.Null(stat.ShortName);
            Assert.Null(stat.StatValues);
            Assert.Null(stat.GameStats);
        }

        [Fact]
        public void Stat_Should_Set_And_Get_Properties()
        {
            var stat = new Stat
            {
                Id = 1,
                Name = "Strength",
                ShortName = "STR",
                StatValues = new List<StatValue>(),
                GameStats = new List<GameStat>()
            };

            Assert.Equal(1, stat.Id);
            Assert.Equal("Strength", stat.Name);
            Assert.Equal("STR", stat.ShortName);
            Assert.NotNull(stat.StatValues);
            Assert.NotNull(stat.GameStats);
        }

        [Fact]
        public void Stat_Name_Should_Not_Exceed_Max_Length()
        {
            var stat = new Stat
            {
                Name = new string('a', 51)
            };

            var context = new ValidationContext(stat, null, null);
            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(stat, context, results, true);

            Assert.False(isValid);
            Assert.Contains(results, v => v.ErrorMessage.Contains("The name cannot be longer than 50 characters."));
        }

        [Fact]
        public void Stat_ShortName_Should_Not_Exceed_Max_Length()
        {
            var stat = new Stat
            {
                ShortName = new string('a', 9)
            };

            var context = new ValidationContext(stat, null, null);
            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(stat, context, results, true);

            Assert.False(isValid);
            Assert.Contains(results, v => v.ErrorMessage.Contains("The shortname for the stat can't be longer than 8 characters."));
        }
    }
}
