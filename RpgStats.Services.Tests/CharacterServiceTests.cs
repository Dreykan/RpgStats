using RpgStats.Dto;

namespace RpgStats.Services.Tests;

public class CharacterServiceTests : IClassFixture<DatabaseFixture>
{
    private readonly CharacterService _service;

    public CharacterServiceTests(DatabaseFixture fixture)
    {
        _service = new CharacterService(fixture.Context);
    }

    [Fact]
    public async Task GetAllCharactersAsync_ReturnsAllCharacters()
    {
        var result = await _service.GetAllCharactersAsync();

        Assert.NotNull(result);
        Assert.Equal(5, result.Count);
    }

    [Fact]
    public async Task GetAllCharactersByGameIdAsync_ReturnsCharactersByGameId()
    {
        var result = await _service.GetAllCharactersByGameIdAsync(2);

        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
    }

    [Fact]
    public async Task GetAllCharactersByGameIdAsync_ReturnsEmptyList_WhenGameNotFound()
    {
        var result = await _service.GetAllCharactersByGameIdAsync(100);

        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetAllCharactersByNameAsync_ReturnsCharactersByName()
    {
        var result = await _service.GetAllCharactersByNameAsync("Char1");

        Assert.NotNull(result);
        Assert.Equal("Char1", result[0].Name);
    }

    [Fact]
    public async Task GetAllCharactersByNameAsync_ReturnsCharactersByNameCaseInsensitive()
    {
        var result = await _service.GetAllCharactersByNameAsync("char1");

        Assert.NotNull(result);
        Assert.Equal("Char1", result[0].Name);
    }

    [Fact]
    public async Task GetAllCharactersByNameAsync_ReturnsEmptyList_WhenNameNotFound()
    {
        var result = await _service.GetAllCharactersByNameAsync("Char4");

        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetCharacterByIdAsync_ReturnsCharacter()
    {
        var result = await _service.GetCharacterByIdAsync(1);

        Assert.NotNull(result);
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
    }

    [Fact]
    public async Task GetCharacterByIdAsync_EmptyDto_WhenIdNotFound()
    {
        var result = await _service.GetCharacterByIdAsync(100);

        Assert.NotNull(result);
        Assert.Equal("", result.Name);
        Assert.Equal(0, result.Id);
        Assert.Null(result.Picture);
        Assert.Null(result.StatValues);
    }

    [Fact]
    public async Task CreateCharacterAsync_ReturnsCreatedCharacter()
    {
        var result = await _service.CreateCharacterAsync(1, new CharacterForCreationDto { Name = "NewChar" });

        Assert.NotNull(result);
        Assert.Equal("NewChar", result.Data?.Name);

        if (result.Data != null)
        {
            await _service.DeleteCharacterAsync(result.Data.Id);
        }
    }

    [Fact]
    public async Task CreateCharacterAsync_Error_WhenGameIdIsNotFound()
    {
        var result = await _service.CreateCharacterAsync(100, new CharacterForCreationDto { Name = "NewChar" });

        Assert.NotNull(result);
        Assert.False(result.Success);
        Assert.Null(result.Data);
        Assert.Equal("Game with ID 100 not found", result.ErrorMessage);
    }

    [Fact]
    public async Task UpdateCharacterAsync_ReturnsCharacter()
    {
        var result = await _service.UpdateCharacterAsync(2, 1, new CharacterForUpdateDto { Name = "UpdatedChar" });

        Assert.NotNull(result);
        Assert.Equal("UpdatedChar", result.Data?.Name);
    }

    [Fact]
    public async Task UpdateCharacterAsync_Error_WhenCharacterNotFound()
    {
        var result = await _service.UpdateCharacterAsync(100, 1, new CharacterForUpdateDto { Name = "UpdatedChar" });

        Assert.NotNull(result);
        Assert.False(result.Success);
        Assert.Equal("Character with ID 100 not found", result.ErrorMessage);
        Assert.Null(result.Data);
    }

    [Fact]
    public async Task UpdateCharacterAsync_Error_WhenGameNotFound()
    {
        var result = await _service.UpdateCharacterAsync(1, 100, new CharacterForUpdateDto { Name = "UpdatedChar" });

        Assert.NotNull(result);
        Assert.False(result.Success);
        Assert.Equal("Game with ID 100 not found", result.ErrorMessage);
        Assert.Null(result.Data);
    }

    [Fact]
    public async Task DeleteCharacterAsync_DeletesCharacter()
    {
        var result = await _service.DeleteCharacterAsync(3);

        Assert.NotNull(result);
        Assert.Equal(3, result.Data?.Id);
    }

    [Fact]
    public async Task DeleteCharacterAsync_Error_WhenCharacterIdNotFound()
    {
        var result = await _service.DeleteCharacterAsync(100);

        Assert.NotNull(result);
        Assert.False(result.Success);
        Assert.Null(result.Data);
        Assert.Equal("Character with ID 100 not found", result.ErrorMessage);
    }

    [Fact]
    public async Task GetAllCharacterDetailDtosAsync_ReturnsAllCharacterDetailDtos()
    {
        var result = await _service.GetAllCharacterDetailDtosAsync();

        Assert.NotNull(result);
        Assert.Equal(5, result.Data?.Count);
    }

    [Fact]
    public async Task GetAllCharacterDetailDtosByGameIdAsync_ReturnsCharactersByGameId()
    {
        var result = await _service.GetAllCharacterDetailDtosByGameIdAsync(2);

        Assert.NotNull(result);
        Assert.Equal(1, result.Data?.Count);
    }

    [Fact]
    public async Task GetAllCharacterDetailDtosByGameIdAsync_Error_WhenGameNotFound()
    {
        var result = await _service.GetAllCharacterDetailDtosByGameIdAsync(100);

        Assert.NotNull(result);
        Assert.False(result.Success);
        Assert.Equal("No characters found", result.ErrorMessage);
        Assert.Null(result.Data);
    }

    [Fact]
    public async Task GetAllCharacterDetailDtosByNameAsync_ReturnsCharactersByName()
    {
        var result = await _service.GetAllCharacterDetailDtosByNameAsync("Char1");

        Assert.NotNull(result);
        Assert.Equal(1, result.Data?.Count);
    }

    [Fact]
    public async Task GetAllCharacterDetailDtosByNameAsync_ReturnsCharactersByNameCaseInsensitive()
    {
        var result = await _service.GetAllCharacterDetailDtosByNameAsync("char1");

        Assert.NotNull(result);
        Assert.Equal(1, result.Data?.Count);
    }

    [Fact]
    public async Task GetAllCharacterDetailDtosByNameAsync_Error_WhenNameNotFound()
    {
        var result = await _service.GetAllCharacterDetailDtosByNameAsync("Char4");

        Assert.NotNull(result);
        Assert.False(result.Success);
        Assert.Null(result.Data);
        Assert.Equal("No characters found", result.ErrorMessage);
    }

    [Fact]
    public async Task GetCharacterDetailDtoById_ReturnsCharacterDetailDto()
    {
        var result = await _service.GetCharacterDetailDtoByIdAsync(1);

        Assert.NotNull(result);
        Assert.Equal("Test1", result.Data?.Name);
    }

    [Fact]
    public async Task GetCharacterDetailDtoById_Error_WhenCharacterIdNotFound()
    {
        var result = await _service.GetCharacterDetailDtoByIdAsync(100);

        Assert.NotNull(result);
        Assert.False(result.Success);
        Assert.Null(result.Data);
        Assert.Equal("Character with ID 100 not found", result.ErrorMessage);
    }
}