using Formula1Import.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Formula1Import.Infrastructure.Persistence.Configurations
{
    public class GrandPrixConfiguration
    {
        public static void Configure(EntityTypeBuilder<GrandPrix> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Name).IsRequired().HasMaxLength(1023);
        }
    }
}
