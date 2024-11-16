namespace RpgStats.Dto.Tests;

public class StatDetailDtoTests
{
    [Fact]
    public void StatDetailDto_Should_Have_Default_Values()
    {
        var dto = new StatDetailDto();
        Assert.Equal(0, dto.Id);
        Assert.Equal(string.Empty, dto.Name);
        Assert.Null(dto.ShortName);
        Assert.Null(dto.StatValueWithCharacterObjectDtos);
        Assert.Null(dto.GameWithoutFkObjectsDtos);
    }

    [Fact]
    public void StatDetailDto_Should_Set_And_Get_Properties()
    {
        var statValueWithCharacterObjectDtos = new List<StatValueWithCharacterObjectDto>();
        var gameWithoutFkObjectsDtos = new List<GameWithoutFkObjectsDto>();

        var dto = new StatDetailDto
        {
            Id = 1,
            Name = "Strength",
            ShortName = "STR",
            StatValueWithCharacterObjectDtos = statValueWithCharacterObjectDtos,
            GameWithoutFkObjectsDtos = gameWithoutFkObjectsDtos
        };

        Assert.Equal(1, dto.Id);
        Assert.Equal("Strength", dto.Name);
        Assert.Equal("STR", dto.ShortName);
        Assert.Equal(statValueWithCharacterObjectDtos, dto.StatValueWithCharacterObjectDtos);
        Assert.Equal(gameWithoutFkObjectsDtos, dto.GameWithoutFkObjectsDtos);
    }

    [Fact]
    public void StatDetailDto_Should_Handle_Null_Values()
    {
        var dto = new StatDetailDto
        {
            Id = 1,
            Name = string.Empty,
            ShortName = null,
            StatValueWithCharacterObjectDtos = null,
            GameWithoutFkObjectsDtos = null
        };

        Assert.Equal(1, dto.Id);
        Assert.Equal(string.Empty, dto.Name);
        Assert.Null(dto.ShortName);
        Assert.Null(dto.StatValueWithCharacterObjectDtos);
        Assert.Null(dto.GameWithoutFkObjectsDtos);
    }
}