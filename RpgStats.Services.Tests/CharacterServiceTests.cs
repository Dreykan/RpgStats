using RpgStats.Domain.Exceptions;
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
        var characters = await _service.GetAllCharactersAsync();
        
        Assert.NotNull(characters);
        Assert.Equal(5, characters.Count);
    }
    
    [Fact]
    public async Task GetAllCharactersByGameIdAsync_ReturnsCharactersByGameId()
    {
        var characters = await _service.GetAllCharactersByGameIdAsync(2);
        
        Assert.NotNull(characters);
        Assert.Equal(2, characters.Count);
    }
    
    [Fact]
    public async Task GetAllCharactersByGameIdAsync_ReturnsEmptyList()
    {
        var characters = await _service.GetAllCharactersByGameIdAsync(100);
        
        Assert.NotNull(characters);
        Assert.Empty(characters);
    }
    
    [Fact]
    public async Task GetAllCharactersByNameAsync_ReturnsCharactersByName()
    {
        var characters = await _service.GetAllCharactersByNameAsync("Char1");
        
        Assert.NotNull(characters);
        Assert.Equal("Char1", characters[0].Name);
        Assert.Single(characters);
    }
    
    [Fact]
    public async Task GetAllCharactersByNameAsync_ReturnsCharactersByNameCaseInsensitive()
    {
        var characters = await _service.GetAllCharactersByNameAsync("char1");
        
        Assert.NotNull(characters);
        Assert.Equal("Char1", characters[0].Name);
        Assert.Single(characters);
    }
    
    [Fact]
    public async Task GetAllCharactersByNameAsync_ReturnsEmptyList()
    {
        var characters = await _service.GetAllCharactersByNameAsync("Char4");
        
        Assert.NotNull(characters);
        Assert.Empty(characters);
    }
    
    [Fact]
    public async Task GetCharacterById_ReturnsCharacter()
    {
        var character = await _service.GetCharacterByIdAsync(1);
        
        Assert.NotNull(character);
        Assert.Equal("Test1", character.Name);
    }
    
    [Fact]
    public async Task GetCharacterById_ThrowsCharacterNotFoundException()
    {
        await Assert.ThrowsAsync<CharacterNotFoundException>(() => _service.GetCharacterByIdAsync(100));
    }
    
    [Fact]
    public async Task CreateCharacterAsync_ReturnsCharacter()
    {
        var character = await _service.CreateCharacterAsync(1, new CharacterForCreationDto { Name = "NewChar" });
        
        Assert.NotNull(character);
        Assert.Equal("NewChar", character.Name);
    }
    
    [Fact]
    public async Task CreateCharacterAsync_ThrowsGameNotFoundException()
    {
        await Assert.ThrowsAsync<GameNotFoundException>(() => _service.CreateCharacterAsync(100, new CharacterForCreationDto { Name = "NewChar" }));
    }
    
    [Fact]
    public async Task CreateCharacterAsync_ThrowsGameNotFoundException_WhenGameIdIsNull()
    {
        await Assert.ThrowsAsync<GameNotFoundException>(() => _service.CreateCharacterAsync(0, new CharacterForCreationDto { Name = "NewChar" }));
    }
    
    [Fact]
    public async Task CreateCharacterAsync_ThrowsGameNotFoundException_WhenGameIdIsNegative()
    {
        await Assert.ThrowsAsync<GameNotFoundException>(() => _service.CreateCharacterAsync(-1, new CharacterForCreationDto { Name = "NewChar" }));
    }
    
    [Fact]
    public async Task CreateCharacterAsync_ThrowsGameNotFoundException_WhenGameIdIsZero()
    {
        await Assert.ThrowsAsync<GameNotFoundException>(() => _service.CreateCharacterAsync(0, new CharacterForCreationDto { Name = "NewChar" }));
    }
    
    [Fact]
    public async Task CreateCharacterAsync_ThrowsGameNotFoundException_WhenGameIdIsNegativeAndGameIsNull()
    {
        await Assert.ThrowsAsync<GameNotFoundException>(() => _service.CreateCharacterAsync(-1, new CharacterForCreationDto { Name = "NewChar" }));
    }
    
    [Fact]
    public async Task UpdateCharacterAsync_ReturnsCharacter()
    {
        var character = await _service.UpdateCharacterAsync(2, 1, new CharacterForUpdateDto { Name = "UpdatedChar" });
        
        Assert.NotNull(character);
        Assert.Equal("UpdatedChar", character.Name);
    }
    
    [Fact]
    public async Task UpdateCharacterAsync_ThrowsCharacterNotFoundException()
    {
        await Assert.ThrowsAsync<CharacterNotFoundException>(() => _service.UpdateCharacterAsync(100, 1, new CharacterForUpdateDto { Name = "UpdatedChar" }));
    }
    
    [Fact]
    public async Task UpdateCharacterAsync_ThrowsGameNotFoundException()
    {
        await Assert.ThrowsAsync<GameNotFoundException>(() => _service.UpdateCharacterAsync(1, 100, new CharacterForUpdateDto { Name = "UpdatedChar" }));
    }
    
    [Fact]
    public async Task UpdateCharacterAsync_ThrowsCharacterNotFoundException_WhenCharacterIdIsZero()
    {
        await Assert.ThrowsAsync<CharacterNotFoundException>(() => _service.UpdateCharacterAsync(0, 1, new CharacterForUpdateDto { Name = "UpdatedChar" }));
    }
    
    [Fact]
    public async Task UpdateCharacterAsync_ThrowsCharacterNotFoundException_WhenCharacterIdIsNegative()
    {
        await Assert.ThrowsAsync<CharacterNotFoundException>(() => _service.UpdateCharacterAsync(-1, 1, new CharacterForUpdateDto { Name = "UpdatedChar" }));
    }
    
    [Fact]
    public async Task UpdateCharacterAsync_ThrowsGameNotFoundException_WhenGameIdIsZero()
    {
        await Assert.ThrowsAsync<GameNotFoundException>(() => _service.UpdateCharacterAsync(1, 0, new CharacterForUpdateDto { Name = "UpdatedChar" }));
    }
    
    [Fact]
    public async Task UpdateCharacterAsync_ThrowsGameNotFoundException_WhenGameIdIsNegative()
    {
        await Assert.ThrowsAsync<GameNotFoundException>(() => _service.UpdateCharacterAsync(1, -1, new CharacterForUpdateDto { Name = "UpdatedChar" }));
    }
    
    [Fact]
    public async Task DeleteCharacterAsync_DeletesCharacter()
    {
        await _service.DeleteCharacterAsync(3);
        
        await Assert.ThrowsAsync<CharacterNotFoundException>(() => _service.GetCharacterByIdAsync(3));
    }
    
    [Fact]
    public async Task DeleteCharacterAsync_DoesNothing_WhenCharacterIdNotFound()
    {
        await _service.DeleteCharacterAsync(100);

        await Assert.ThrowsAsync<CharacterNotFoundException>(() => _service.GetCharacterByIdAsync(100));
    }
    
    [Fact]
    public async Task DeleteCharacterAsync_DoesNothing_WhenCharacterIdIsZero()
    {
        await _service.DeleteCharacterAsync(0);

        await Assert.ThrowsAsync<CharacterNotFoundException>(() => _service.GetCharacterByIdAsync(0));
    }
    
    [Fact]
    public async Task DeleteCharacterAsync_DoesNothing_WhenCharacterIdIsNegative()
    {
        await _service.DeleteCharacterAsync(-1);

        await Assert.ThrowsAsync<CharacterNotFoundException>(() => _service.GetCharacterByIdAsync(-1));
    }
    
    [Fact]
    public async Task GetAllCharacterDetailDtosAsync_ReturnsAllCharacterDetailDtos()
    {
        var characterDetailDtos = await _service.GetAllCharacterDetailDtosAsync();
        
        Assert.NotNull(characterDetailDtos);
        Assert.Equal(6, characterDetailDtos.Count);
    }
    
    [Fact]
    public async Task GetAllCharacterDetailDtosByGameIdAsync_ReturnsCharactersByGameId()
    {
        var characterDetailDtos = await _service.GetAllCharacterDetailDtosByGameIdAsync(2);
        
        Assert.NotNull(characterDetailDtos);
        Assert.Single(characterDetailDtos);
    }
    
    [Fact]
    public async Task GetAllCharacterDetailDtosByGameIdAsync_ReturnsEmptyList()
    {
        var characterDetailDtos = await _service.GetAllCharacterDetailDtosByGameIdAsync(100);
        
        Assert.NotNull(characterDetailDtos);
        Assert.Empty(characterDetailDtos);
    }
    
    [Fact]
    public async Task GetAllCharacterDetailDtosByNameAsync_ReturnsCharactersByName()
    {
        var characterDetailDtos = await _service.GetAllCharacterDetailDtosByNameAsync("Char1");
        
        Assert.NotNull(characterDetailDtos);
        Assert.Single(characterDetailDtos);
    }
    
    [Fact]
    public async Task GetAllCharacterDetailDtosByNameAsync_ReturnsCharactersByNameCaseInsensitive()
    {
        var characterDetailDtos = await _service.GetAllCharacterDetailDtosByNameAsync("char1");
        
        Assert.NotNull(characterDetailDtos);
        Assert.Single(characterDetailDtos);
    }
    
    [Fact]
    public async Task GetAllCharacterDetailDtosByNameAsync_ReturnsEmptyList()
    {
        var characterDetailDtos = await _service.GetAllCharacterDetailDtosByNameAsync("Char4");
        
        Assert.NotNull(characterDetailDtos);
        Assert.Empty(characterDetailDtos);
    }
    
    [Fact]
    public async Task GetCharacterDetailDtoById_ReturnsCharacterDetailDto()
    {
        var characterDetailDto = await _service.GetCharacterDetailDtoByIdAsync(1);
        
        Assert.NotNull(characterDetailDto);
        Assert.Equal("Test1", characterDetailDto.Name);
    }
    
    [Fact]
    public async Task GetCharacterDetailDtoById_GetEmptyCharacterDetailDto()
    {
        var characterDetailDto = await _service.GetCharacterDetailDtoByIdAsync(100);
        
        Assert.NotNull(characterDetailDto);
        Assert.Null(characterDetailDto.Name);
    }
}