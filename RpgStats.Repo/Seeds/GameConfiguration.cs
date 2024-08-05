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
    internal class GameConfiguration : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> builder)
        {
            builder.HasData(
                new Game { Id = 1, Name = "Final Fantasy VII - Remake", Picture = File.ReadAllBytes("images/FFVII-Remake-Cover.jpg")},
                new Game { Id = 2, Name = "Octopath Traveler", Picture = File.ReadAllBytes("images/OctopathTraveler.jpg")}
            );
        }
    }
}
