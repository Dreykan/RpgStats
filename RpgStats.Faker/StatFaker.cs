using Bogus;
using RpgStats.Dto;

namespace RpgStats.Faker;

public sealed class StatFaker : Faker<StatForCreationDto>
{
    public StatFaker()
    {
        RuleFor(x => x.Name, f => f.Lorem.Word());
        RuleFor(x => x.ShortName, f => f.Lorem.Word()[..2]);
    }
}