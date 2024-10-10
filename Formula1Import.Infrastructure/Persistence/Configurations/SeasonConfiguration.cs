using Formula1Import.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Formula1Import.Infrastructure.Persistence.Configurations
{
    public class SeasonConfiguration
    {
        public static void Configure(EntityTypeBuilder<Season> builder)
        {
            builder.HasKey(e => e.Year);
            builder.Property(e => e.Year).ValueGeneratedNever();
            builder.Property(e => e.WikipediaUrl).IsRequired().HasMaxLength(1023);
        }
    }
}
