using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RpgStats.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgStats.Repo.Seeds;

internal class StatConfiguration : IEntityTypeConfiguration<Stat>
{
    public void Configure(EntityTypeBuilder<Stat> builder)
    {
        builder.HasData(
            new Stat { Id = 1, Name = "Hitpoints", ShortName = "HP" },
            new Stat { Id = 2, Name = "Manapoints", ShortName = "MP" },
            new Stat { Id = 3, Name = "Physical Attack", ShortName = "PATK" },
            new Stat { Id = 4, Name = "Magic Attack", ShortName = "MATK" },
            new Stat { Id = 5, Name = "Physical Defense", ShortName = "PDEF" },
            new Stat { Id = 6, Name = "Magic Defense", ShortName = "MDEF" },
            new Stat { Id = 7, Name = "Strength", ShortName = "STR" },
            new Stat { Id = 8, Name = "Magic", ShortName = "MAG" },
            new Stat { Id = 9, Name = "Vitality", ShortName = "VIT" },
            new Stat { Id = 10, Name = "Spirit", ShortName = "SPI" },
            new Stat { Id = 11, Name = "Luck", ShortName = "LU" },
            new Stat { Id = 12, Name = "Speed", ShortName = "SPD" },
            new Stat { Id = 13, Name = "Accuracy", ShortName = "ACC" },
            new Stat { Id = 14, Name = "Critical", ShortName = "CRIT" },
            new Stat { Id = 15, Name = "Evasion", ShortName = "EVA" }
        );
    }
}