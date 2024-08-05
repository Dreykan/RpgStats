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
    internal class PlatformConfiguration : IEntityTypeConfiguration<Platform>
    {
        public void Configure(EntityTypeBuilder<Platform> builder)
        {
            builder.HasData(
                new Platform { Id = 1, Name = "PC", PlatformGames = new List<PlatformGame>() },
                new Platform { Id = 2, Name = "PlayStation 1", PlatformGames = new List<PlatformGame>() },
                new Platform { Id = 3, Name = "PlayStation 2", PlatformGames = new List<PlatformGame>() },
                new Platform { Id = 4, Name = "PlayStation 3", PlatformGames = new List<PlatformGame>() },
                new Platform { Id = 5, Name = "PlayStation 4", PlatformGames = new List<PlatformGame>() },
                new Platform { Id = 6, Name = "PlayStation 5", PlatformGames = new List<PlatformGame>() },
                new Platform { Id = 7, Name = "Xbox Series X/S", PlatformGames = new List<PlatformGame>() },
                new Platform { Id = 8, Name = "Nintendo Switch", PlatformGames = new List<PlatformGame>() }
                
            );
        }
    }
}
