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
    internal class CharacterConfiguration : IEntityTypeConfiguration<Character>
    {
        public void Configure(EntityTypeBuilder<Character> builder)
        {
            builder.HasData(
                new Character { Id = 1, Name = "Cloud Strife", GameId = 1, Picture = File.ReadAllBytes("../RpgStats.Repo/Seeds/images/CloudStrife.jpg") },
                new Character { Id = 2, Name = "Tifa Lockhart", GameId = 1, Picture = File.ReadAllBytes("../RpgStats.Repo/Seeds/images/TifaLockhart.jpg") },
                new Character { Id = 3, Name = "Aerith Gainsborough", GameId = 1, Picture = File.ReadAllBytes("../RpgStats.Repo/Seeds/images/AerithGainsborough.jpg") },
                new Character { Id = 4, Name = "Ophilia Clement", GameId = 2, Picture = File.ReadAllBytes("../RpgStats.Repo/Seeds/images/OphiliaClement.jpg") },
                new Character { Id = 5, Name = "Cyrus Albright", GameId = 2, Picture = File.ReadAllBytes("../RpgStats.Repo/Seeds/images/CyrusAlbright.jpg") },
                new Character { Id = 6, Name = "Tressa Colzione", GameId = 2, Picture = File.ReadAllBytes("../RpgStats.Repo/Seeds/images/TressaColzione.jpg") }
            );
        }
    }
}
