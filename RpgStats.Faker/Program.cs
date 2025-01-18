// See https://aka.ms/new-console-template for more information

using System.Net.Http.Headers;
using System.Net.Http.Json;
using RpgStats.Dto;
using RpgStats.Faker;

var random = new Random();
const int platformCount = 20;
const int gameCount = 50;

var platformFaker = new PlatformFaker();
var gameFaker = new GameFaker();

var platformList = new List<PlatformForCreationDto>();
var gameList = new List<GameForCreationDto>();

var fakedPlatforms = platformFaker.Generate(platformCount);
var fakedGames = gameFaker.Generate(gameCount);

platformList.AddRange(fakedPlatforms);
gameList.AddRange(fakedGames);


var httpClient = new HttpClient();
httpClient.BaseAddress = new Uri("https://localhost:7073");
httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

foreach (var platform in platformList)
{
    Console.WriteLine(platform.Name);

    // Save to database
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

foreach (var game in gameList)
{
    Console.WriteLine(game.Name);

    // Save to database
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

        var characterCount = random.Next(3, 10);
        for (int i = 0; i < characterCount; i++)
        {
            var characterFaker = new CharacterFaker();
            var character = characterFaker.Generate();
            await httpClient.PostAsJsonAsync($"api/Character/CreateCharacter/{createdGame?.Data?.Id}", character);
        }
    }
    else
    {
        Console.WriteLine($"Game not created: {game.Name}");
    }
}