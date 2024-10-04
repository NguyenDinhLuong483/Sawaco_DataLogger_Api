
namespace SawacoApi.Intrastructure.Context.Configuration
{
    public class LoggerEntityTypeConfiguration : IEntityTypeConfiguration<Logger>
    {
        public void Configure(EntityTypeBuilder<Logger> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasMaxLength(20);
            builder.Property(x => x.Longtitude).IsRequired();
            builder.Property(x => x.Latitude).IsRequired();
            builder.Property(x => x.Name).HasMaxLength(256).IsRequired();
            builder.Property(x => x.Battery);
            builder.Property(x => x.Temperature);
            builder.Property(x => x.Stolen);
            builder.Property(x => x.Bluetooth).HasMaxLength(10);
            builder.Property(x => x.TimeStamp);
            builder.HasMany(x => x.StolenLines).WithOne(x => x.Logger).HasForeignKey(x => x.LoggerId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
