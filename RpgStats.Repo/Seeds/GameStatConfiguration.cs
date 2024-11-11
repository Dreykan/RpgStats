using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RpgStats.Domain.Entities;

namespace RpgStats.Repo.Seeds;

internal class GameStatConfiguration : IEntityTypeConfiguration<GameStat>
{
    public void Configure(EntityTypeBuilder<GameStat> builder)
    {
        builder.HasData(
            new GameStat { Id = 1, GameId = 1, StatId = 1 },
            new GameStat { Id = 2, GameId = 1, StatId = 2 },
            new GameStat { Id = 3, GameId = 1, StatId = 3 },
            new GameStat { Id = 4, GameId = 1, StatId = 4 },
            new GameStat { Id = 5, GameId = 1, StatId = 5 },
            new GameStat { Id = 6, GameId = 1, StatId = 6 },
            new GameStat { Id = 7, GameId = 1, StatId = 7 },
            new GameStat { Id = 8, GameId = 1, StatId = 8 },
            new GameStat { Id = 9, GameId = 1, StatId = 9 },
            new GameStat { Id = 10, GameId = 1, StatId = 10 },
            new GameStat { Id = 11, GameId = 1, StatId = 11 },
            new GameStat { Id = 12, GameId = 1, StatId = 12 },
            new GameStat { Id = 13, GameId = 2, StatId = 1 },
            new GameStat { Id = 14, GameId = 2, StatId = 2 },
            new GameStat { Id = 15, GameId = 2, StatId = 3 },
            new GameStat { Id = 16, GameId = 2, StatId = 4 },
            new GameStat { Id = 17, GameId = 2, StatId = 5 },
            new GameStat { Id = 18, GameId = 2, StatId = 6 },
            new GameStat { Id = 19, GameId = 2, StatId = 12 },
            new GameStat { Id = 20, GameId = 2, StatId = 13 },
            new GameStat { Id = 21, GameId = 2, StatId = 14 },
            new GameStat { Id = 22, GameId = 2, StatId = 15 }
        );
    }
}