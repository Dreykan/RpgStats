using RpgStats.Domain.Entities;
using System.ComponentModel.DataAnnotations;
// ReSharper disable UseObjectOrCollectionInitializer

namespace RpgStats.Domain.Tests.Entities
{
    public class StatTests
    {
        [Fact]
        public void Stat_Id_ShouldBeSetAndRetrievedCorrectly()
        {
            var stat = new Stat();

            stat.Id = 12345;

            Assert.Equal(12345, stat.Id);
        }

        [Fact]
        public void Stat_Name_ShouldBeSetAndRetrievedCorrectly()
        {
            var stat = new Stat();

            stat.Name = "TestStat";

            Assert.Equal("TestStat", stat.Name);
        }

        [Fact]
        public void Stat_ShortName_ShouldBeSetAndRetrievedCorrectly()
        {
            var stat = new Stat();

            stat.ShortName = "TS";

            Assert.Equal("TS", stat.ShortName);
        }

        [Fact]
        public void Stat_StatValues_ShouldBeSetAndRetrievedCorrectly()
        {
            var stat = new Stat();

            stat.StatValues = new List<StatValue> { new StatValue { Id = 12345, Value = 10 } };

            Assert.Equal(12345, stat.StatValues.First().Id);
            Assert.Equal(10, stat.StatValues.First().Value);
        }

        [Fact]
        public void Stat_GameStats_ShouldBeSetAndRetrievedCorrectly()
        {
            var stat = new Stat();

            stat.GameStats = new List<GameStat> { new GameStat { StatId = 12345 } };

            Assert.Equal(12345, stat.GameStats.First().StatId);
        }

        [Fact]
        public void Stat_Name_RequiredValidation()
        {
            var stat = new Stat();

            var validationResults = ValidateModel(stat);

            Assert.NotEmpty(validationResults);
            Assert.Contains(validationResults,
                v => v.ErrorMessage != null && v.ErrorMessage.Contains("A name for the stat is required."));
        }

        [Fact]
        public void Stat_Name_LengthValidation()
        {
            var stat = new Stat
            {
                Name = new string('a', 51)
            };

            var validationResults = ValidateModel(stat);

            Assert.NotEmpty(validationResults);
            Assert.Contains(validationResults,
                v => v.ErrorMessage != null && v.ErrorMessage.Contains("The name cannot be longer than 50 characters."));
        }

        [Fact]
        public void Stat_ShortName_LengthValidation()
        {
            var stat = new Stat
            {
                ShortName = new string('a', 9)
            };

            var validationResults = ValidateModel(stat);

            Assert.NotEmpty(validationResults);
            Assert.Contains(validationResults,
                v => v.ErrorMessage != null && v.ErrorMessage.Contains("The shortname for the stat can't be longer than 8 characters."));
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