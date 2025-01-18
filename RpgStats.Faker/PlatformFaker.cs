using Bogus;
using RpgStats.Dto;

namespace RpgStats.Faker;

public sealed class PlatformFaker : Faker<PlatformForCreationDto>
{
    public PlatformFaker()
    {
        RuleFor(x => x.Name, f => f.Commerce.Product());
    }
}