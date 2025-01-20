using System.Net.Http.Json;
using RpgStats.Dto;

namespace RpgStats.Faker;

public static class MainFaker
{
    public static void Run()
    {
        var random = new Random();
        var httpClient = new HttpClient { BaseAddress = new Uri("http://localhost:10000/api") };

        RunPlatformFaker(10, httpClient).Wait();
        RunStatFaker(20, httpClient).Wait();
        RunGameFaker(30, httpClient).Wait();
        RunPlatformGameFaker(random, httpClient).Wait();
        RunGameStatFaker(random, httpClient).Wait();
        RunCharacterFaker(random, httpClient).Wait();
        RunStatValueFaker(random, httpClient).Wait();

        httpClient.Dispose();

        Console.WriteLine("Faking done.");
    }

    private static async Task RunPlatformFaker(int count, HttpClient httpClient)
    {
        var platformFaker = new PlatformFaker();
        var platformList = platformFaker.Generate(count);
        foreach (var platform in platformList)
        {
            await httpClient.PostAsJsonAsync("api/Platform/CreatePlatform", platform);
        }

        Console.WriteLine("PlatformFaker done.");
    }

    private static async Task RunStatFaker(int count, HttpClient httpClient)
    {
        var statFaker = new StatFaker();
        var statList = statFaker.Generate(count);
        foreach (var stat in statList)
        {
            await httpClient.PostAsJsonAsync("api/Stat/CreateStat", stat);
        }

        Console.WriteLine("StatFaker done.");
    }

    private static async Task RunGameFaker(int count, HttpClient httpClient)
    {
        var gameFaker = new GameFaker();
        var gameList = gameFaker.Generate(count);
        foreach (var game in gameList)
        {
            await httpClient.PostAsJsonAsync("api/Game/CreateGame", game);
        }

        Console.WriteLine("GameFaker done.");
    }

    private static async Task RunPlatformGameFaker(Random random, HttpClient httpClient)
    {
        var games = await GetGames(httpClient);
        var platforms = await GetPlatforms(httpClient);
        foreach (var game in games)
        {
            var platformGameCount = random.Next(1, 5);
            for (int i = 0; i < platformGameCount; i++)
            {
                var platformId = random.Next(1, platforms.Count);
                await httpClient.PostAsJsonAsync($"api/PlatformGame/CreatePlatformGame/{platformId}/{game?.Id}", "");
            }
        }

        Console.WriteLine("PlatformGameFaker done.");
    }

    private static async Task RunGameStatFaker(Random random, HttpClient httpClient)
    {
        var games = await GetGames(httpClient);
        var stats = await GetStats(httpClient);
        foreach (var game in games)
        {
            var gameStatCount = random.Next(stats.Count / 4, stats.Count / 2);
            var startStatId = random.Next(1, stats.Count - gameStatCount);
            for (int i = 0; i < gameStatCount; i++)
            {
                var gameStatFaker = new GameStatFaker();
                var gameStat = gameStatFaker.Generate();
                gameStat.StatId = startStatId + i;
                gameStat.GameId = game?.Id ?? 0;
                gameStat.SortIndex = i + 1;
                await httpClient.PostAsJsonAsync($"api/GameStat/CreateGameStat", gameStat);
            }
        }

        Console.WriteLine("GameStatFaker done.");
    }

    private static async Task RunCharacterFaker(Random random, HttpClient httpClient)
    {
        var games = await GetGames(httpClient);
        foreach (var game in games)
        {
            var characterCount = random.Next(3, 10);
            for (int i = 0; i < characterCount; i++)
            {
                var characterFaker = new CharacterFaker();
                var character = characterFaker.Generate();
                await httpClient.PostAsJsonAsync($"api/Character/CreateCharacter/{game?.Id}", character);
            }
        }

        Console.WriteLine("CharacterFaker done.");
    }

    private static async Task RunStatValueFaker(Random random, HttpClient httpClient)
    {
        var statValues = new List<StatValueForCreationDto>();
        var characters = await GetCharacters(httpClient);
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

            var statValueLevelCount = random.Next(50, 100);
            foreach (var stat in stats)
            {
                var value = random.Next(20, 40);
                for (int i = 1; i <= statValueLevelCount; i++)
                {
                    var statValue = new StatValueForCreationDto
                    {
                        Value = value + random.Next(3, 10),
                        Level = i,
                        ContainedBonusNum = 0,
                        ContainedBonusPercent = 0,
                        CharacterId = character.Id,
                        StatId = stat.Id
                    };

                    statValues.Add(statValue);
                    value = statValue.Value;
                }
            }
        }
        await httpClient.PostAsJsonAsync($"api/StatValue/CreateStatValues", statValues);

        Console.WriteLine("StatValueFaker done.");
    }

    private static async Task<List<GameDto>> GetGames(HttpClient httpClient)
    {
        var gameResponse = await httpClient.GetAsync("api/Game/GetGames");
        var gameResult = await gameResponse.Content.ReadFromJsonAsync<RpgStatsResponse<List<GameDto>>>();
        if (gameResult == null || gameResult.Success == false || gameResult.Data == null)
        {
            return new List<GameDto>();
        }

        return gameResult.Data;
    }

    private static async Task<List<PlatformDto>> GetPlatforms(HttpClient httpClient)
    {
        var platformResponse = await httpClient.GetAsync("api/Platform/GetPlatforms");
        var platformResult = await platformResponse.Content.ReadFromJsonAsync<RpgStatsResponse<List<PlatformDto>>>();
        if (platformResult == null || platformResult.Success == false || platformResult.Data == null)
        {
            return new List<PlatformDto>();
        }

        return platformResult.Data;
    }

    private static async Task<List<StatDto>> GetStats(HttpClient httpClient)
    {
        var statResponse = await httpClient.GetAsync("api/Stat/GetStats");
        var statResult = await statResponse.Content.ReadFromJsonAsync<RpgStatsResponse<List<StatDto>>>();
        if (statResult == null || statResult.Success == false || statResult.Data == null)
        {
            return new List<StatDto>();
        }

        return statResult.Data;
    }

    private static async Task<List<CharacterDto>> GetCharacters(HttpClient httpClient)
    {
        var characterResponse = await httpClient.GetAsync("api/Character/GetCharacters");
        var characterResult = await characterResponse.Content.ReadFromJsonAsync<RpgStatsResponse<List<CharacterDto>>>();
        if (characterResult == null || characterResult.Success == false || characterResult.Data == null)
        {
            return new List<CharacterDto>();
        }

        return characterResult.Data;
    }
}