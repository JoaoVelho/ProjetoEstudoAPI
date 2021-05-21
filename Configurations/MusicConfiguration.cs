using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjetoEstudoAPI.Models;

namespace ProjetoEstudoAPI.Configurations
{
    public class MusicConfiguration : IEntityTypeConfiguration<Music>
    {
        public void Configure(EntityTypeBuilder<Music> builder)
        {
            builder.HasData(
                new Music {
                    Id = 1,
                    Name = "amari",
                    AlbumId = 1
                },
                new Music {
                    Id = 2,
                    Name = "pride is the devil",
                    AlbumId = 1
                },
                new Music {
                    Id = 3,
                    Name = "No Role Modelz",
                    AlbumId = 2
                },
                new Music {
                    Id = 4,
                    Name = "Note to Self",
                    AlbumId = 2
                },
                new Music {
                    Id = 5,
                    Name = "Breathe",
                    AlbumId = 3
                },
                new Music {
                    Id = 6,
                    Name = "Eclipse",
                    AlbumId = 3
                },
                new Music {
                    Id = 7,
                    Name = "Mother",
                    AlbumId = 4
                },
                new Music {
                    Id = 8,
                    Name = "Hey You",
                    AlbumId = 4
                }
            );
        }
    }
}