namespace RpgStats.Dto.Tests;

public class PlatformDetailDtoTests
{
    [Fact]
    public void PlatformDetailDto_Should_Have_Default_Values()
    {
        var dto = new PlatformDetailDto();
        Assert.Equal(0, dto.Id);
        Assert.Equal(string.Empty, dto.Name);
        Assert.Null(dto.GameWithoutFkObjectsDtos);
    }

    [Fact]
    public void PlatformDetailDto_Should_Set_And_Get_Properties()
    {
        var dto = new PlatformDetailDto
        {
            Id = 1,
            Name = "Platform Name",
            GameWithoutFkObjectsDtos = new List<GameWithoutFkObjectsDto>()
        };

        Assert.Equal(1, dto.Id);
        Assert.Equal("Platform Name", dto.Name);
        Assert.NotNull(dto.GameWithoutFkObjectsDtos);
    }

    [Fact]
    public void PlatformDetailDto_Should_Handle_Empty_Name()
    {
        var dto = new PlatformDetailDto
        {
            Id = 1,
            Name = string.Empty,
            GameWithoutFkObjectsDtos = new List<GameWithoutFkObjectsDto>()
        };

        Assert.Equal(1, dto.Id);
        Assert.Equal(string.Empty, dto.Name);
        Assert.NotNull(dto.GameWithoutFkObjectsDtos);
    }

    [Fact]
    public void PlatformDetailDto_Should_Handle_Empty_GameWithoutFkObjectsDtos()
    {
        var dto = new PlatformDetailDto
        {
            Id = 1,
            Name = "Platform Name",
            GameWithoutFkObjectsDtos = new List<GameWithoutFkObjectsDto>()
        };

        Assert.Equal(1, dto.Id);
        Assert.Equal("Platform Name", dto.Name);
        Assert.Empty(dto.GameWithoutFkObjectsDtos);
    }
}