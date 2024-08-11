namespace RpgStats.Dto.Tests;

public class CharacterDetailDtoTests
{
    [Fact]
    public void CharacterDetailDto_Should_Have_Default_Values()
    {
        var dto = new CharacterDetailDto();
        Assert.Equal(0, dto.Id);
        Assert.Null(dto.Name);
        Assert.Null(dto.Picture);
        Assert.Null(dto.GameWithoutFkObjectsDto);
        Assert.Null(dto.StatValuesWithStatObjectDtos);
    }

    [Fact]
    public void CharacterDetailDto_Should_Allow_Setting_Values()
    {
        var gameDto = new GameWithoutFkObjectsDto();
        var statValues = new List<StatValueWithStatObjectDto>();
        var dto = new CharacterDetailDto
        {
            Id = 1,
            Name = "Test Character",
            Picture = new byte[] { 1, 2, 3 },
            GameWithoutFkObjectsDto = gameDto,
            StatValuesWithStatObjectDtos = statValues
        };
        Assert.Equal(1, dto.Id);
        Assert.Equal("Test Character", dto.Name);
        Assert.Equal(new byte[] { 1, 2, 3 }, dto.Picture);
        Assert.Equal(gameDto, dto.GameWithoutFkObjectsDto);
        Assert.Equal(statValues, dto.StatValuesWithStatObjectDtos);
    }

    [Fact]
    public void CharacterDetailDto_Should_Handle_Null_Values()
    {
        var dto = new CharacterDetailDto
        {
            Id = 1,
            Name = null,
            Picture = null,
            GameWithoutFkObjectsDto = null,
            StatValuesWithStatObjectDtos = null
        };
        Assert.Equal(1, dto.Id);
        Assert.Null(dto.Name);
        Assert.Null(dto.Picture);
        Assert.Null(dto.GameWithoutFkObjectsDto);
        Assert.Null(dto.StatValuesWithStatObjectDtos);
    }
}