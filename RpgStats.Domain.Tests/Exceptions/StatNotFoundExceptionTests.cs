using RpgStats.Domain.Exceptions;
using Xunit;

namespace RpgStats.Domain.Tests.Exceptions
{
    public class StatNotFoundExceptionTests
    {
        [Fact]
        public void StatNotFoundException_Creation_Success()
        {
            long statId = 456;

            var exception = new StatNotFoundException(statId);

            Assert.NotNull(exception);
        }
        
        [Fact]
        public void StatNotFoundException_Type_IsCorrect()
        {
            long statId = 456;

            var exception = new StatNotFoundException(statId);

            Assert.IsType<StatNotFoundException>(exception);
        }

        [Fact]
        public void StatNotFoundException_Message_IsCorrect()
        {
            long statId = 456;
            string expectedMessage = $"The Stat with the identifier {statId} was not found.";

            var exception = new StatNotFoundException(statId);

            Assert.Equal(expectedMessage, exception.Message);
        }
    }
}
