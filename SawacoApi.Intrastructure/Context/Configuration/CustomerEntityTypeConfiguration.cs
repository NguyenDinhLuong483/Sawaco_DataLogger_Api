

namespace SawacoApi.Intrastructure.Context.Configuration
{
    public class CustomerEntityTypeConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(x => x.PhoneNumber);
            builder.Property(x => x.PhoneNumber).IsRequired();
            builder.Property(x => x.UserName).IsRequired();
            builder.Property(x => x.Password).IsRequired();

            builder.HasMany(x => x.GPSDevices).WithOne(x => x.Customer).HasForeignKey(x => x.CustomerPhoneNumber).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(x => x.GPSObjects).WithOne(x => x.Customer).HasForeignKey(x => x.CustomerPhoneNumber).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(x => x.Notification).WithOne(x => x.Customer).HasForeignKey(x => x.CustomerPhoneNumber).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
