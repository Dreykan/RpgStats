using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RpgStats.Domain.Entities;

namespace RpgStats.Repo.Seeds;

internal class PlatformGameConfiguration : IEntityTypeConfiguration<PlatformGame>
{
    public void Configure(EntityTypeBuilder<PlatformGame> builder)
    {
        builder.HasData(
            new PlatformGame { Id = 1, GameId = 1, PlatformId = 1 },
            new PlatformGame { Id = 2, GameId = 1, PlatformId = 5 },
            new PlatformGame { Id = 3, GameId = 1, PlatformId = 6 },
            new PlatformGame { Id = 4, GameId = 2, PlatformId = 1 },
            new PlatformGame { Id = 5, GameId = 2, PlatformId = 5 },
            new PlatformGame { Id = 6, GameId = 2, PlatformId = 6 },
            new PlatformGame { Id = 7, GameId = 2, PlatformId = 7 },
            new PlatformGame { Id = 8, GameId = 2, PlatformId = 8 }
        );
    }
}