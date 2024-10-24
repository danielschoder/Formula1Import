using Formula1Import.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Formula1Import.Infrastructure.Persistence.Configurations;

public class ResultConfiguration
{
    public static void Configure(EntityTypeBuilder<Result> builder)
    {
        builder.HasKey(e => e.Id);
    }
}
