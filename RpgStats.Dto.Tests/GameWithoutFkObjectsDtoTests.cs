namespace RpgStats.Dto.Tests;

public class GameWithoutFkObjectsDtoTests
{
    [Fact]
    public void Id_ShouldBeSetAndRetrievedCorrectly()
    {
        var dto = new GameWithoutFkObjectsDto { Id = 1 };
        Assert.Equal(1, dto.Id);
    }

    [Fact]
    public void Name_ShouldBeSetAndRetrievedCorrectly()
    {
        var dto = new GameWithoutFkObjectsDto { Name = "Test Game" };
        Assert.Equal("Test Game", dto.Name);
    }

    [Fact]
    public void Picture_ShouldBeSetAndRetrievedCorrectly()
    {
        var pictureData = new byte[] { 1, 2, 3 };
        var dto = new GameWithoutFkObjectsDto { Picture = pictureData };
        Assert.Equal(pictureData, dto.Picture);
    }

    [Fact]
    public void Name_ShouldHandleEmptyValue()
    {
        var dto = new GameWithoutFkObjectsDto();
        Assert.Equal(string.Empty, dto.Name);
    }

    [Fact]
    public void Picture_ShouldHandleNullValue()
    {
        var dto = new GameWithoutFkObjectsDto { Picture = null };
        Assert.Null(dto.Picture);
    }
}