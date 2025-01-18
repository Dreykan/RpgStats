using Bogus;
using RpgStats.Dto;

namespace RpgStats.Faker;

public sealed class GameFaker : Faker<GameForCreationDto>
{
    public GameFaker()
    {
        var client = new HttpClient();

        RuleFor(x => x.Name, f => f.Random.Word());
        RuleFor(x => x.Picture, f => client.GetByteArrayAsync(f.Image.LoremFlickrUrl().ToString()).Result);
    }
}