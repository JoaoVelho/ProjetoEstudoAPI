using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjetoEstudoAPI.Models;

namespace ProjetoEstudoAPI.Configurations
{
    public class ArtistConfiguration : IEntityTypeConfiguration<Artist>
    {
        public void Configure(EntityTypeBuilder<Artist> builder)
        {
            builder.HasData(
                new Artist {
                    Id = "64d94610-25d3-4d82-a0fc-27351ead67de",
                    Name = "J Cole",
                    UserName = "jcole@email.com",
                    NormalizedUserName = "JCOLE@EMAIL.COM",
                    Email = "jcole@email.com",
                    NormalizedEmail = "JCOLE@EMAIL.COM",
                    EmailConfirmed = true,
                    PasswordHash = "AQAAAAEAACcQAAAAEEf+5gVrIZBhIAorpI/6QtDq2dIGLutY3QAPeOZiunTzzfFCHcIOUEUFbIy5Twut5g==",
                    SecurityStamp = "4TTHTLZP6K7R4KWM27OFNZCVOUWCBGAP",
                    ConcurrencyStamp = "28a2a66c-8f50-4e01-9986-6f951bd8b96e",
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnabled = true,
                    AccessFailedCount = 0
                },
                new Artist {
                    Id = "a2631013-893d-4fbc-baed-e5a24001c907",
                    Name = "Pink Floyd",
                    UserName = "pfloyd@email.com",
                    NormalizedUserName = "PFLOYD@EMAIL.COM",
                    Email = "pfloyd@email.com",
                    NormalizedEmail = "PFLOYD@EMAIL.COM",
                    EmailConfirmed = true,
                    PasswordHash = "AQAAAAEAACcQAAAAEBiA5KYQ3DEgqjd1skx33Ru0D2WjV5hanIy2NIlqtac6y1mwo8JS7v0nzXbSFlGXRw==",
                    SecurityStamp = "UU3ZV2L3GZL24ZNDXZWOA5XDC2QKVJAB",
                    ConcurrencyStamp = "5d75c2f8-a019-49b7-ae9f-4355b6e0fa17",
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnabled = true,
                    AccessFailedCount = 0
                }
            );
        }
    }
}