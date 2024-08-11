using RpgStats.Dto;

namespace RpgStats.Dto.Tests
{
    public class PlatformWithoutFkObjectsDtoTests
    {
        [Fact]
        public void Id_ShouldBeSetAndRetrievedCorrectly()
        {
            var dto = new PlatformWithoutFkObjectsDto { Id = 1 };
            Assert.Equal(1, dto.Id);
        }

        [Fact]
        public void Name_ShouldBeSetAndRetrievedCorrectly()
        {
            var dto = new PlatformWithoutFkObjectsDto { Name = "Test Platform" };
            Assert.Equal("Test Platform", dto.Name);
        }

        [Fact]
        public void Name_ShouldAllowNull()
        {
            var dto = new PlatformWithoutFkObjectsDto { Name = null };
            Assert.Null(dto.Name);
        }

        [Fact]
        public void Name_ShouldAllowEmptyString()
        {
            var dto = new PlatformWithoutFkObjectsDto { Name = string.Empty };
            Assert.Equal(string.Empty, dto.Name);
        }
    }
}