// See https://aka.ms/new-console-template for more information

using System.Net.Http.Headers;
using System.Net.Http.Json;
using RpgStats.Dto;
using RpgStats.Faker;

var random = new Random();
const int platformCount = 20;
const int gameCount = 50;
const int statCount = 16;

var platformFaker = new PlatformFaker();
var gameFaker = new GameFaker();
var statFaker = new StatFaker();

var platformList = new List<PlatformForCreationDto>();
var gameList = new List<GameForCreationDto>();
var statList = new List<StatForCreationDto>();

var fakedPlatforms = platformFaker.Generate(platformCount);
var fakedGames = gameFaker.Generate(gameCount);
var fakedStats = statFaker.Generate(statCount);

platformList.AddRange(fakedPlatforms);
gameList.AddRange(fakedGames);
statList.AddRange(fakedStats);


var httpClient = new HttpClient();
httpClient.BaseAddress = new Uri("https://localhost:7073");
httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

// Save to database
foreach (var platform in platformList)
{
    var response = await httpClient.PostAsJsonAsync("api/Platform/CreatePlatform", platform);
    if (response.IsSuccessStatusCode)
    {
        Console.WriteLine($"Platform created: {platform.Name}");
    }
    else
    {
        Console.WriteLine($"Platform not created: {platform.Name}");
    }
}

foreach (var stat in statList)
{
    var response = await httpClient.PostAsJsonAsync("api/Stat/CreateStat", stat);
    if (response.IsSuccessStatusCode)
    {
        Console.WriteLine($"Stat created: {stat.Name}");
    }
    else
    {
        Console.WriteLine($"Stat not created: {stat.Name}");
    }
}

foreach (var game in gameList)
{
    var response = await httpClient.PostAsJsonAsync("api/Game/CreateGame", game);
    if (response.IsSuccessStatusCode)
    {
        var createdGame = await response.Content.ReadFromJsonAsync<RpgStatsResponse<GameDto>>();

        var platformGameCount = random.Next(1, 5);
        for (int i = 0; i < platformGameCount; i++)
        {
            var platformId = random.Next(1, platformCount);
            await httpClient.PostAsJsonAsync($"api/PlatformGame/CreatePlatformGame/{platformId}/{createdGame?.Data?.Id}", "");
        }

        var gameStatCount = random.Next(statCount / 4, statCount / 2);
        var startStatId = random.Next(1, statCount - gameStatCount);
        for (int i = 0; i < gameStatCount; i++)
        {
            var gameStatFaker = new GameStatFaker();
            var gameStat = gameStatFaker.Generate();
            gameStat.StatId = startStatId + i;
            gameStat.GameId = createdGame?.Data?.Id ?? 0;
            gameStat.SortIndex = i + 1;
            await httpClient.PostAsJsonAsync($"api/GameStat/CreateGameStat", gameStat);
        }

        var characterCount = random.Next(3, 10);
        for (int i = 0; i < characterCount; i++)
        {
            var characterFaker = new CharacterFaker();
            var character = characterFaker.Generate();
            await httpClient.PostAsJsonAsync($"api/Character/CreateCharacter/{createdGame?.Data?.Id}", character);
        }

        Console.WriteLine($"Game created: {game.Name}");
    }
    else
    {
        Console.WriteLine($"Game not created: {game.Name}");
    }
}

Console.WriteLine("Creating stat values...");
var charactersResponse = await httpClient.GetAsync($"api/Character/GetCharacters");
var charactersResult = await charactersResponse.Content.ReadFromJsonAsync<RpgStatsResponse<List<CharacterDto>>>();
var characters = charactersResult?.Data;
if (characters is null)
{
    Console.WriteLine("No characters found.");
    return;
}
foreach (var character in characters)
{
    var statsResponse = await httpClient.GetAsync($"api/Stat/GetStatsByGameId/{character.GameId}");
    var statsResult = await statsResponse.Content.ReadFromJsonAsync<RpgStatsResponse<List<StatDto>>>();
    var stats = statsResult?.Data;
    if (stats is null)
    {
        Console.WriteLine("No stats found.");
        return;
    }

    var statValueLevelCount = random.Next(1, 50);

    foreach (var stat in stats)
    {
        var value = random.Next(20, 40);
        for (int i = 1; i <= statValueLevelCount; i++)
        {
            var statValue = new StatValueForCreationDto
            {
                Value = value + random.Next(1, 5),
                Level = i,
                ContainedBonusNum = 0,
                ContainedBonusPercent = 0
            };
            await httpClient.PostAsJsonAsync($"api/StatValue/CreateStatValue/{character.Id}/{stat.Id}", statValue);
            value = statValue.Value;
        }
    }
}

Console.WriteLine("Done.");