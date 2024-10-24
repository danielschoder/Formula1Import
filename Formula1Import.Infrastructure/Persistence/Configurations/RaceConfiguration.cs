using Formula1Import.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Formula1Import.Infrastructure.Persistence.Configurations;

public class RaceConfiguration
{
    public static void Configure(EntityTypeBuilder<Race> builder)
    {
        builder.HasKey(e => e.Id);
    }
}
