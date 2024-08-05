using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RpgStats.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgStats.Repo.Seeds
{
    internal class StatValueConfiguration : IEntityTypeConfiguration<StatValue>
    {
        public void Configure(EntityTypeBuilder<StatValue> builder)
        {
            builder.HasData(
                new StatValue { Id = 1, CharacterId = 1, Level = 7, StatId = 1, Value = 1026, ContainedBonusNum = 0, ContainedBonusPercent = 0 },
                new StatValue { Id = 1, CharacterId = 1, Level = 7, StatId = 2, Value = 30, ContainedBonusNum = 0, ContainedBonusPercent = 0 },
                new StatValue { Id = 1, CharacterId = 1, Level = 7, StatId = 3, Value = 54, ContainedBonusNum = 0, ContainedBonusPercent = 0 },
                new StatValue { Id = 1, CharacterId = 1, Level = 7, StatId = 4, Value = 52, ContainedBonusNum = 0, ContainedBonusPercent = 0 },
                new StatValue { Id = 1, CharacterId = 1, Level = 7, StatId = 5, Value = 21, ContainedBonusNum = 0, ContainedBonusPercent = 0 },
                new StatValue { Id = 1, CharacterId = 1, Level = 7, StatId = 6, Value = 21, ContainedBonusNum = 0, ContainedBonusPercent = 0 },
                new StatValue { Id = 1, CharacterId = 1, Level = 7, StatId = 7, Value = 16, ContainedBonusNum = 0, ContainedBonusPercent = 0 },
                new StatValue { Id = 1, CharacterId = 1, Level = 7, StatId = 8, Value = 15, ContainedBonusNum = 0, ContainedBonusPercent = 0 },
                new StatValue { Id = 1, CharacterId = 1, Level = 7, StatId = 9, Value = 11, ContainedBonusNum = 0, ContainedBonusPercent = 0 },
                new StatValue { Id = 1, CharacterId = 1, Level = 7, StatId = 10, Value = 11, ContainedBonusNum = 0, ContainedBonusPercent = 0 },
                new StatValue { Id = 1, CharacterId = 1, Level = 7, StatId = 11, Value = 18, ContainedBonusNum = 0, ContainedBonusPercent = 0 },
                new StatValue { Id = 1, CharacterId = 1, Level = 7, StatId = 12, Value = 12, ContainedBonusNum = 0, ContainedBonusPercent = 0 },
                new StatValue { Id = 1, CharacterId = 1, Level = 8, StatId = 1, Value = 1082, ContainedBonusNum = 0, ContainedBonusPercent = 0 },
                new StatValue { Id = 1, CharacterId = 1, Level = 8, StatId = 2, Value = 31, ContainedBonusNum = 0, ContainedBonusPercent = 0 },
                new StatValue { Id = 1, CharacterId = 1, Level = 8, StatId = 3, Value = 58, ContainedBonusNum = 0, ContainedBonusPercent = 0 },
                new StatValue { Id = 1, CharacterId = 1, Level = 8, StatId = 4, Value = 56, ContainedBonusNum = 0, ContainedBonusPercent = 0 },
                new StatValue { Id = 1, CharacterId = 1, Level = 8, StatId = 5, Value = 23, ContainedBonusNum = 0, ContainedBonusPercent = 0 },
                new StatValue { Id = 1, CharacterId = 1, Level = 8, StatId = 6, Value = 23, ContainedBonusNum = 0, ContainedBonusPercent = 0 },
                new StatValue { Id = 1, CharacterId = 1, Level = 8, StatId = 7, Value = 18, ContainedBonusNum = 0, ContainedBonusPercent = 0 },
                new StatValue { Id = 1, CharacterId = 1, Level = 8, StatId = 8, Value = 17, ContainedBonusNum = 0, ContainedBonusPercent = 0 },
                new StatValue { Id = 1, CharacterId = 1, Level = 8, StatId = 9, Value = 13, ContainedBonusNum = 0, ContainedBonusPercent = 0 },
                new StatValue { Id = 1, CharacterId = 1, Level = 8, StatId = 10, Value = 13, ContainedBonusNum = 0, ContainedBonusPercent = 0 },
                new StatValue { Id = 1, CharacterId = 1, Level = 8, StatId = 11, Value = 19, ContainedBonusNum = 0, ContainedBonusPercent = 0 },
                new StatValue { Id = 1, CharacterId = 1, Level = 8, StatId = 12, Value = 13, ContainedBonusNum = 0, ContainedBonusPercent = 0 }
            );
        }
    }
}
