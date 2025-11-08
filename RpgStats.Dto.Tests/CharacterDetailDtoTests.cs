namespace RpgStats.Dto.Tests;

public class CharacterDetailDtoTests
{
    [Fact]
    public void CharacterDetailDto_Should_Have_Default_Values()
    {
        var dto = new CharacterDetailDto
        {
            Name = "TestCharacter"
        };
        Assert.Equal(0, dto.Id);
        Assert.Null(dto.Picture);
        Assert.Null(dto.Note);
        Assert.Null(dto.Game);
        Assert.Null(dto.StatValues);
    }

    [Fact]
    public void CharacterDetailDto_Should_Allow_Setting_Values()
    {
        var gameDto = new List<GameStatDto>();
        var statValues = new List<StatValueDto>();
        var dto = new CharacterDetailDto
        {
            Id = 1,
            Name = "Test Character",
            Picture = new byte[] { 1, 2, 3 },
            Note = "This is a test note.",
            GameStats = gameDto,
            StatValues = statValues
        };
        Assert.Equal(1, dto.Id);
        Assert.Equal("Test Character", dto.Name);
        Assert.Equal(new byte[] { 1, 2, 3 }, dto.Picture);
        Assert.Equal("This is a test note.", dto.Note);
        Assert.Equal(gameDto, dto.GameStats);
        Assert.Equal(statValues, dto.StatValues);
    }

    [Fact]
    public void CharacterDetailDto_Should_Handle_Null_Values()
    {
        var dto = new CharacterDetailDto
        {
            Id = 1,
            Name = "TestCharacter",
            Picture = null,
            Note = null,
            GameStats = null,
            StatValues = null
        };
        Assert.Equal(1, dto.Id);
        Assert.Null(dto.Picture);
        Assert.Null(dto.Note);
        Assert.Null(dto.GameStats);
        Assert.Null(dto.StatValues);
    }
}