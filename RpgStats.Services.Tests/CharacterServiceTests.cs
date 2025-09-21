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
        Assert.Equal("NewChar", result.Name);

        await _service.DeleteCharacterAsync(result.Id);
    }

    [Fact]
    public async Task CreateCharacterAsync_Error_WhenGameIdIsNotFound()
    {
        await Assert.ThrowsAsync<ArgumentException>(async () =>
            await _service.CreateCharacterAsync(100, new CharacterForCreationDto { Name = "NewChar" }));
    }

    [Fact]
    public async Task UpdateCharacterAsync_ReturnsCharacter()
    {
        var result = await _service.UpdateCharacterAsync(2, 1, new CharacterForUpdateDto { Name = "UpdatedChar" });

        Assert.NotNull(result);
        Assert.Equal("UpdatedChar", result.Name);
    }

    [Fact]
    public async Task UpdateCharacterAsync_Error_WhenCharacterNotFound()
    {
        await Assert.ThrowsAsync<ArgumentException>(async () =>
            await _service.UpdateCharacterAsync(100, 1, new CharacterForUpdateDto { Name = "UpdatedChar" }));
    }

    [Fact]
    public async Task UpdateCharacterAsync_Error_WhenGameNotFound()
    {
        await Assert.ThrowsAsync<ArgumentException>(async () =>
            await _service.UpdateCharacterAsync(1, 100, new CharacterForUpdateDto { Name = "UpdatedChar" }));
    }

    [Fact]
    public async Task DeleteCharacterAsync_DeletesCharacter()
    {
        var result = await _service.DeleteCharacterAsync(3);

        Assert.NotNull(result);
        Assert.Equal(3, result.Id);
    }

    [Fact]
    public async Task DeleteCharacterAsync_Error_WhenCharacterIdNotFound()
    {
        await Assert.ThrowsAsync<ArgumentException>(async () =>
            await _service.DeleteCharacterAsync(100));
    }
}