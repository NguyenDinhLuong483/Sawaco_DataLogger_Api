
namespace SawacoApi.Intrastructure.Context.Configuration
{
    public class GPSObjectEntityTypeConfiguration : IEntityTypeConfiguration<GPSObject>
    {
        public void Configure(EntityTypeBuilder<GPSObject> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Name);
            builder.Property(x => x.Longitude);
            builder.Property(x => x.Latitude);
            builder.Property(x => x.Description);
            builder.Property(x => x.ImagePath);
            builder.Property(x => x.SafeRadius);
            builder.Property(x => x.Size);
            builder.Property(x => x.Connected);

            builder.HasMany(x => x.ObjectPositionHistories).WithOne(x => x.GPSObject).HasForeignKey(x => x.GPSObjectId).OnDelete(DeleteBehavior.Cascade);   
        }
    }
}
