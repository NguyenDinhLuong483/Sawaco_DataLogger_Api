

namespace SawacoApi.Intrastructure.Context.Configuration
{
    public class NotificationEntityTypeConfiguration : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.Title);
            builder.Property(x => x.Description);
            builder.Property(x => x.Timestamp);
            builder.Property(x => x.IsAcknowledge);
        }
    }
}
