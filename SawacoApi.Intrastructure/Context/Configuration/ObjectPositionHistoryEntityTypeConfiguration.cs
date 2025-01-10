

namespace SawacoApi.Intrastructure.Context.Configuration
{
    public class ObjectPositionHistoryEntityTypeConfiguration : IEntityTypeConfiguration<ObjectPositionHistory>
    {
        public void Configure(EntityTypeBuilder<ObjectPositionHistory> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Longitude);
            builder.Property(x => x.Latitude);
            builder.Property(x => x.Timestamp);
        }
    }
}
