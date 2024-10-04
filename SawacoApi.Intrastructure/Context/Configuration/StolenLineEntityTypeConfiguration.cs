
namespace SawacoApi.Intrastructure.Context.Configuration
{
    public class StolenLineEntityTypeConfiguration : IEntityTypeConfiguration<StolenLine>
    {
        public void Configure(EntityTypeBuilder<StolenLine> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Longtitude).IsRequired();
            builder.Property(x => x.Latitude).IsRequired();
            builder.Property(x => x.Battery).IsRequired();
            builder.Property(x => x.TimeStamp).IsRequired();
        }
    }
}
