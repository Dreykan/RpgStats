using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RpgStats.Domain.Entities;

namespace RpgStats.Repo.Seeds;

internal class GameStatConfiguration : IEntityTypeConfiguration<GameStat>
{
    public void Configure(EntityTypeBuilder<GameStat> builder)
    {
        builder.HasData(
            new GameStat { Id = 1, GameId = 1, StatId = 1, SortIndex = 1, CustomStatName = "Trefferpunkte", CustomStatShortName = "TP"},
            new GameStat { Id = 2, GameId = 1, StatId = 2, SortIndex = 2, CustomStatName = "Manapunkte", CustomStatShortName = "MP"},
            new GameStat { Id = 3, GameId = 1, StatId = 3, SortIndex = 3, CustomStatName = "Physische Attacke", CustomStatShortName = "PATK"},
            new GameStat { Id = 4, GameId = 1, StatId = 4, SortIndex = 4, CustomStatName = "Magische Attacke", CustomStatShortName = "MATK"},
            new GameStat { Id = 5, GameId = 1, StatId = 5, SortIndex = 5, CustomStatName = "Physische Abwehr", CustomStatShortName = "PABW"},
            new GameStat { Id = 6, GameId = 1, StatId = 6, SortIndex = 6, CustomStatName = "Magische Abwehr", CustomStatShortName = "MABW"},
            new GameStat { Id = 7, GameId = 1, StatId = 7, SortIndex = 7, CustomStatName = "Stärke", CustomStatShortName = "STR"},
            new GameStat { Id = 8, GameId = 1, StatId = 8, SortIndex = 8, CustomStatName = "Magie", CustomStatShortName = "MAG"},
            new GameStat { Id = 9, GameId = 1, StatId = 9, SortIndex = 9, CustomStatName = "Vitalität", CustomStatShortName = "VIT"},
            new GameStat { Id = 10, GameId = 1, StatId = 10, SortIndex = 10, CustomStatName = "Willenskraft", CustomStatShortName = "WIL"},
            new GameStat { Id = 11, GameId = 1, StatId = 11, SortIndex = 11, CustomStatName = "Glück", CustomStatShortName = "GLÜ"},
            new GameStat { Id = 12, GameId = 1, StatId = 12, SortIndex = 12, CustomStatName = "Schnelligkeit", CustomStatShortName = "SCHN"},
            new GameStat { Id = 13, GameId = 2, StatId = 1, SortIndex = 1, CustomStatName = "Gesundheitspunkte", CustomStatShortName = "GP"},
            new GameStat { Id = 14, GameId = 2, StatId = 16, SortIndex = 2, CustomStatName = "Fertigkeitspunkte", CustomStatShortName = "FP"},
            new GameStat { Id = 15, GameId = 2, StatId = 3, SortIndex = 3, CustomStatName = "Physischer Angriff", CustomStatShortName = "PANG"},
            new GameStat { Id = 16, GameId = 2, StatId = 4, SortIndex = 4, CustomStatName = "Elementarer Angriff", CustomStatShortName = "EANG"},
            new GameStat { Id = 17, GameId = 2, StatId = 5, SortIndex = 5, CustomStatName = "Physische Verteidigung", CustomStatShortName = "PVER"},
            new GameStat { Id = 18, GameId = 2, StatId = 6, SortIndex = 6, CustomStatName = "Elementare Verteidigung", CustomStatShortName = "EVER"},
            new GameStat { Id = 19, GameId = 2, StatId = 13, SortIndex = 7, CustomStatName = "Genauigkeit", CustomStatShortName = "GEN"},
            new GameStat { Id = 20, GameId = 2, StatId = 12, SortIndex = 8, CustomStatName = "Tempo", CustomStatShortName = "TEM"},
            new GameStat { Id = 21, GameId = 2, StatId = 14, SortIndex = 9, CustomStatName = "Kritisch", CustomStatShortName = "KRIT"},
            new GameStat { Id = 22, GameId = 2, StatId = 15, SortIndex = 10, CustomStatName = "Ausweichen", CustomStatShortName = "AUSW"}
        );
    }
}