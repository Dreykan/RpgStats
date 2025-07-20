using Bogus;
using RpgStats.Dto;

namespace RpgStats.Faker;

public sealed class CharacterFaker : Faker<CharacterForCreationDto>
{
    public CharacterFaker()
    {
        var client = new HttpClient();

        RuleFor(x => x.Name, f => f.Person.FullName);
        // RuleFor(x => x.Picture, f => client.GetByteArrayAsync(f.Image.LoremFlickrUrl().ToString()).Result);
    }
}