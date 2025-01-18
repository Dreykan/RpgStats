using Bogus;
using RpgStats.Dto;

namespace RpgStats.Faker;

public sealed class GameStatFaker : Faker<GameStatForCreationDto>
{
    public GameStatFaker()
    {
        RuleFor(x => x.CustomStatName, f => f.Lorem.Word());
        RuleFor(x => x.CustomStatShortName, f => f.Lorem.Random.AlphaNumeric(3).ToUpper());
    }
}