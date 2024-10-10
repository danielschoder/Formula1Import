using Formula1Import.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Formula1Import.Infrastructure.Persistence.Configurations;

public class SessionConfiguration
{
    public static void Configure(EntityTypeBuilder<Session> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.SessionTypeId).HasConversion<int>();
    }
}
