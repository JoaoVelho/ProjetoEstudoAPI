using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjetoEstudoAPI.Models;

namespace ProjetoEstudoAPI.Configurations
{
    public class AlbumConfiguration : IEntityTypeConfiguration<Album>
    {
        public void Configure(EntityTypeBuilder<Album> builder)
        {
            builder.HasData(
                new Album {
                    Id = 1,
                    Name = "The Off-Season",
                    ArtistId = "64d94610-25d3-4d82-a0fc-27351ead67de"
                },
                new Album {
                    Id = 2,
                    Name = "2014 Forest Hills Drive",
                    ArtistId = "64d94610-25d3-4d82-a0fc-27351ead67de"
                },
                new Album {
                    Id = 3,
                    Name = "The Dark Side of the Moon",
                    ArtistId = "a2631013-893d-4fbc-baed-e5a24001c907"
                },
                new Album {
                    Id = 4,
                    Name = "The Wall",
                    ArtistId = "a2631013-893d-4fbc-baed-e5a24001c907"
                }
            );
        }
    }
}