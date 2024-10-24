using Formula1Import.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Formula1Import.Infrastructure.Persistence.Configurations;

public class SessionTypeConfiguration
{
    public static void Configure(EntityTypeBuilder<SessionType> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).ValueGeneratedNever();
        builder.Property(e => e.Description).IsRequired().HasMaxLength(255);
    }
}
