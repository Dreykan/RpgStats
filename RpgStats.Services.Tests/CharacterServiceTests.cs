using Microsoft.EntityFrameworkCore;
using RpgStats.Domain.Entities;
using RpgStats.Domain.Exceptions;
using RpgStats.Dto;
using RpgStats.Repo;

namespace RpgStats.Services.Tests;

public class CharacterServiceTests(DatabaseFixture fixture) : IClassFixture<DatabaseFixture>
{
    private readonly CharacterService _service = new(fixture.Context);

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
}