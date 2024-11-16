namespace RpgStats.Dto.Tests;

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
    public void Name_ShouldAllowEmptyString()
    {
        var dto = new PlatformWithoutFkObjectsDto();
        Assert.Equal(string.Empty, dto.Name);
    }
}