using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SawacoApi.Domain.Models;

namespace SawacoApi.Domain.Persistances.Context.Configurations
{
    public class StolenLineEntityTypeConfiguration : IEntityTypeConfiguration<StolenLine>
    {
        public void Configure(EntityTypeBuilder<StolenLine> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Longtitude).IsRequired();
            builder.Property(x => x.Latitude).IsRequired();
            builder.Property(x => x.TimeStamp).IsRequired();
        }
    }
}
