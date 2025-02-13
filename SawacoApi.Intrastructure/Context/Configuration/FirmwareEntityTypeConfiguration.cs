
namespace SawacoApi.Intrastructure.Context.Configuration
{
    public class FirmwareEntityTypeConfiguration : IEntityTypeConfiguration<Firmware>
    {
        public void Configure(EntityTypeBuilder<Firmware> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.Version).IsRequired();
            builder.Property(x => x.ReleaseDate).IsRequired();
            builder.Property(x => x.FilePath).IsRequired();
        }
    }
}
