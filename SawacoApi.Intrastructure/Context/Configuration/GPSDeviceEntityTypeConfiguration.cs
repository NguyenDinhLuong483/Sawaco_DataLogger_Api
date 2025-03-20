
namespace SawacoApi.Intrastructure.Context.Configuration
{
    public class GPSDeviceEntityTypeConfiguration : IEntityTypeConfiguration<GPSDevice>
    {
        public void Configure(EntityTypeBuilder<GPSDevice> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasMaxLength(20);
            builder.Property(x => x.Longitude).IsRequired();
            builder.Property(x => x.Latitude).IsRequired();
            builder.Property(x => x.Name).HasMaxLength(256).IsRequired();
            builder.Property(x => x.ImagePath).IsRequired();
            builder.Property(x => x.Battery);
            builder.Property(x => x.Temperature);
            builder.Property(x => x.Stolen);
            builder.Property(x => x.Bluetooth).HasMaxLength(10);
            builder.Property(x => x.TimeStamp);
            builder.Property(x => x.AlarmTime);
            builder.Property(x => x.Emergency);
            builder.Property(x => x.SMSNumber);
            builder.Property(x => x.Package);
            builder.Property(x => x.RegistationDate);
            builder.Property(x => x.ExpirationDate);

            builder.HasMany(x => x.StolenLines).WithOne(x => x.GPSDevice).HasForeignKey(x => x.GPSDeviceId).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(x => x.DevicePositionHistories).WithOne(x => x.GPSDevice).HasForeignKey(x => x.GPSDeviceId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
