using RpgStats.Domain.Entities;
using System.ComponentModel.DataAnnotations;
// ReSharper disable UseObjectOrCollectionInitializer

namespace RpgStats.Domain.Tests.Entities
{
    public class StatValueTests
    {
        [Fact]
        public void StatValue_Id_ShouldBeSetAndRetrievedCorrectly()
        {
            var statValue = new StatValue();

            statValue.Id = 12345;

            Assert.Equal(12345, statValue.Id);
        }

        [Fact]
        public void StatValue_Level_ShouldBeSetAndRetrievedCorrectly()
        {
            var statValue = new StatValue();

            statValue.Level = 10;

            Assert.Equal(10, statValue.Level);
        }

        [Fact]
        public void StatValue_CharacterId_ShouldBeSetAndRetrievedCorrectly()
        {
            var statValue = new StatValue();

            statValue.CharacterId = 12345;

            Assert.Equal(12345, statValue.CharacterId);
        }

        [Fact]
        public void StatValue_Character_ShouldBeSetAndRetrievedCorrectly()
        {
            var statValue = new StatValue();

            statValue.Character = new Character { Id = 12345, Name = "TestCharacter" };

            Assert.Equal(12345, statValue.Character.Id);
            Assert.Equal("TestCharacter", statValue.Character.Name);
        }

        [Fact]
        public void StatValue_StatId_ShouldBeSetAndRetrievedCorrectly()
        {
            var statValue = new StatValue();

            statValue.StatId = 12345;

            Assert.Equal(12345, statValue.StatId);
        }

        [Fact]
        public void StatValue_Stat_ShouldBeSetAndRetrievedCorrectly()
        {
            var statValue = new StatValue();

            statValue.Stat = new Stat { Id = 12345, Name = "TestStat" };

            Assert.Equal(12345, statValue.Stat.Id);
            Assert.Equal("TestStat", statValue.Stat.Name);
        }

        [Fact]
        public void StatValue_Value_ShouldBeSetAndRetrievedCorrectly()
        {
            var statValue = new StatValue();

            statValue.Value = 100;

            Assert.Equal(100, statValue.Value);
        }

        [Fact]
        public void StatValue_ContainedBonusNum_ShouldBeSetAndRetrievedCorrectly()
        {
            var statValue = new StatValue();

            statValue.ContainedBonusNum = 5;

            Assert.Equal(5, statValue.ContainedBonusNum);
        }

        [Fact]
        public void StatValue_ContainedBonusPercent_ShouldBeSetAndRetrievedCorrectly()
        {
            var statValue = new StatValue();

            statValue.ContainedBonusPercent = 20;

            Assert.Equal(20, statValue.ContainedBonusPercent);
        }

        [Fact]
        public void StatValue_Level_RequiredValidation()
        {
            var statValue = new StatValue
            {
                CharacterId = 1,
                StatId = 1,
                Value = 10,
                ContainedBonusNum = 5,
                ContainedBonusPercent = 20
            };

            var validationResults = ValidateModel(statValue);

            Assert.NotEmpty(validationResults);
            Assert.Contains(validationResults,
                v => v.ErrorMessage != null && v.ErrorMessage.Contains("A Level entry is required."));
        }

        [Fact]
        public void StatValue_CharacterId_RequiredValidation()
        {
            var statValue = new StatValue
            {
                Level = 10,
                StatId = 1,
                Value = 10,
                ContainedBonusNum = 5,
                ContainedBonusPercent = 20
            };

            var validationResults = ValidateModel(statValue);

            Assert.NotEmpty(validationResults);
            Assert.Contains(validationResults,
                v => v.ErrorMessage != null && v.ErrorMessage.Contains("An Character-Entry is required."));
        }

        [Fact]
        public void StatValue_StatId_RequiredValidation()
        {
            var statValue = new StatValue
            {
                Level = 10,
                CharacterId = 1,
                Value = 10,
                ContainedBonusNum = 5,
                ContainedBonusPercent = 20
            };

            var validationResults = ValidateModel(statValue);

            Assert.NotEmpty(validationResults);
            Assert.Contains(validationResults,
                v => v.ErrorMessage != null && v.ErrorMessage.Contains("A Stat-Entry is required."));
        }

        [Fact]
        public void StatValue_Value_RequiredValidation()
        {
            var statValue = new StatValue
            {
                Level = 10,
                CharacterId = 1,
                StatId = 1,
                ContainedBonusNum = 5,
                ContainedBonusPercent = 20
            };

            var validationResults = ValidateModel(statValue);

            Assert.NotEmpty(validationResults);
            Assert.Contains(validationResults,
                v => v.ErrorMessage != null && v.ErrorMessage.Contains("A value for the stat entry is required."));
        }

        [Fact]
        public void StatValue_ContainedBonusNum_RequiredValidation()
        {
            var statValue = new StatValue
            {
                Level = 10,
                CharacterId = 1,
                StatId = 1,
                Value = 10,
                ContainedBonusPercent = 20
            };

            var validationResults = ValidateModel(statValue);

            Assert.NotEmpty(validationResults);
            Assert.Contains(validationResults,
                v => v.ErrorMessage != null && v.ErrorMessage.Contains("A bonus contained herein in numbers for this entry is required."));
        }

        [Fact]
        public void StatValue_ContainedBonusPercent_RequiredValidation()
        {
            var statValue = new StatValue
            {
                Level = 10,
                CharacterId = 1,
                StatId = 1,
                Value = 10,
                ContainedBonusNum = 5
            };

            var validationResults = ValidateModel(statValue);

            Assert.NotEmpty(validationResults);
            Assert.Contains(validationResults,
                v => v.ErrorMessage != null && v.ErrorMessage.Contains("A bonus contained herein in percent for this entry is required."));
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