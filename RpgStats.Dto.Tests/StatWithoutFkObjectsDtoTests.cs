namespace RpgStats.Dto.Tests;

public class StatWithoutFkObjectsDtoTests
{
    [Fact]
    public void Id_Should_Be_Set_And_Retrieved_Correctly()
    {
        var dto = new StatWithoutFkObjectsDto { Id = 1 };
        Assert.Equal(1, dto.Id);
    }

    [Fact]
    public void Name_Should_Be_Set_And_Retrieved_Correctly()
    {
        var dto = new StatWithoutFkObjectsDto { Name = "Strength" };
        Assert.Equal("Strength", dto.Name);
    }

    [Fact]
    public void ShortName_Should_Be_Set_And_Retrieved_Correctly()
    {
        var dto = new StatWithoutFkObjectsDto { ShortName = "STR" };
        Assert.Equal("STR", dto.ShortName);
    }

    [Fact]
    public void Name_Should_Be_Empty_By_Default()
    {
        var dto = new StatWithoutFkObjectsDto();
        Assert.Equal(string.Empty, dto.Name);
    }

    [Fact]
    public void ShortName_Should_Be_Null_By_Default()
    {
        var dto = new StatWithoutFkObjectsDto();
        Assert.Null(dto.ShortName);
    }
}