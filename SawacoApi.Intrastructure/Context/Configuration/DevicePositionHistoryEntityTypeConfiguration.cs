
namespace SawacoApi.Intrastructure.Context.Configuration
{
    public class DevicePositionHistoryEntityTypeConfiguration : IEntityTypeConfiguration<DevicePositionHistory>
    {
        public void Configure(EntityTypeBuilder<DevicePositionHistory> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Longitude);
            builder.Property(x => x.Latitude);
            builder.Property(x => x.Timestamp);
        }
    }
}
