namespace RpgStats.Dto.Tests;

public class GameDetailDtoTests
{
    [Fact]
    public void GameDetailDto_Should_Have_Default_Values()
    {
        var dto = new GameDetailDto();

        Assert.Equal(0, dto.Id);
        Assert.Null(dto.Name);
        Assert.Null(dto.Picture);
        Assert.Null(dto.CharacterWithoutFkObjectsDtos);
        Assert.Null(dto.StatWithoutFkObjectsDtos);
        Assert.Null(dto.PlatformWithoutFkObjectsDtos);
    }

    [Fact]
    public void GameDetailDto_Should_Set_And_Get_Properties()
    {
        var characters = new List<CharacterWithoutFkObjectsDto>();
        var stats = new List<StatWithoutFkObjectsDto>();
        var platforms = new List<PlatformWithoutFkObjectsDto>();

        var dto = new GameDetailDto
        {
            Id = 1,
            Name = "Test Game",
            Picture = new byte[] { 1, 2, 3 },
            CharacterWithoutFkObjectsDtos = characters,
            StatWithoutFkObjectsDtos = stats,
            PlatformWithoutFkObjectsDtos = platforms
        };

        Assert.Equal(1, dto.Id);
        Assert.Equal("Test Game", dto.Name);
        Assert.Equal(new byte[] { 1, 2, 3 }, dto.Picture);
        Assert.Equal(characters, dto.CharacterWithoutFkObjectsDtos);
        Assert.Equal(stats, dto.StatWithoutFkObjectsDtos);
        Assert.Equal(platforms, dto.PlatformWithoutFkObjectsDtos);
    }

    [Fact]
    public void GameDetailDto_Should_Handle_Null_Values()
    {
        var dto = new GameDetailDto
        {
            Id = 1,
            Name = null,
            Picture = null,
            CharacterWithoutFkObjectsDtos = null,
            StatWithoutFkObjectsDtos = null,
            PlatformWithoutFkObjectsDtos = null
        };

        Assert.Equal(1, dto.Id);
        Assert.Null(dto.Name);
        Assert.Null(dto.Picture);
        Assert.Null(dto.CharacterWithoutFkObjectsDtos);
        Assert.Null(dto.StatWithoutFkObjectsDtos);
        Assert.Null(dto.PlatformWithoutFkObjectsDtos);
    }
}