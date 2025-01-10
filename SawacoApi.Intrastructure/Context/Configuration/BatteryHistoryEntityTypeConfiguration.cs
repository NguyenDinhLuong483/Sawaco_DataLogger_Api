

namespace SawacoApi.Intrastructure.Context.Configuration
{
    public class BatteryHistoryEntityTypeConfiguration : IEntityTypeConfiguration<BatteryHistory>
    {
        public void Configure(EntityTypeBuilder<BatteryHistory> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.Value).IsRequired();
            builder.Property(x => x.Timestamp).IsRequired();    
        }
    }
}
