namespace RpgStats.Dto.Tests;

public class CharacterWithoutFkObjectsDtoTests
{
    [Fact]
    public void Id_ShouldBeSetAndRetrievedCorrectly()
    {
        var dto = new CharacterWithoutFkObjectsDto { Id = 1 };
        Assert.Equal(1, dto.Id);
    }

    [Fact]
    public void Name_ShouldBeSetAndRetrievedCorrectly()
    {
        var dto = new CharacterWithoutFkObjectsDto { Name = "Hero" };
        Assert.Equal("Hero", dto.Name);
    }

    [Fact]
    public void Name_ShouldBeEmpty()
    {
        var dto = new CharacterWithoutFkObjectsDto();
        Assert.Equal(string.Empty, dto.Name);
    }

    [Fact]
    public void Picture_ShouldBeSetAndRetrievedCorrectly()
    {
        var pictureData = new byte[] { 1, 2, 3 };
        var dto = new CharacterWithoutFkObjectsDto { Picture = pictureData };
        Assert.Equal(pictureData, dto.Picture);
    }

    [Fact]
    public void Picture_ShouldAllowNull()
    {
        var dto = new CharacterWithoutFkObjectsDto { Picture = null };
        Assert.Null(dto.Picture);
    }
}