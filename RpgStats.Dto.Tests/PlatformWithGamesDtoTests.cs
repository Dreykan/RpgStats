namespace RpgStats.Dto.Tests;

public class PlatformWithGamesDtoTests
{
    [Fact]
    public void PlatformDetailDto_Should_Have_Default_Values()
    {
        var dto = new PlatformWithGamesDto();
        Assert.Equal(0, dto.Id);
        Assert.Equal(string.Empty, dto.Name);
        Assert.Null(dto.GameDtos);
    }

    [Fact]
    public void PlatformDetailDto_Should_Set_And_Get_Properties()
    {
        var dto = new PlatformWithGamesDto
        {
            Id = 1,
            Name = "Platform Name",
            GameDtos = new List<GameWithoutFkObjectsDto>()
        };

        Assert.Equal(1, dto.Id);
        Assert.Equal("Platform Name", dto.Name);
        Assert.NotNull(dto.GameDtos);
    }

    [Fact]
    public void PlatformDetailDto_Should_Handle_Empty_Name()
    {
        var dto = new PlatformWithGamesDto
        {
            Id = 1,
            Name = string.Empty,
            GameDtos = new List<GameWithoutFkObjectsDto>()
        };

        Assert.Equal(1, dto.Id);
        Assert.Equal(string.Empty, dto.Name);
        Assert.NotNull(dto.GameDtos);
    }

    [Fact]
    public void PlatformDetailDto_Should_Handle_Empty_GameWithoutFkObjectsDtos()
    {
        var dto = new PlatformWithGamesDto
        {
            Id = 1,
            Name = "Platform Name",
            GameDtos = new List<GameWithoutFkObjectsDto>()
        };

        Assert.Equal(1, dto.Id);
        Assert.Equal("Platform Name", dto.Name);
        Assert.Empty(dto.GameDtos);
    }
}