
namespace SawacoApi.Intrastructure.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<GPSDevice> GPSDevices { get; set; }
        public DbSet<StolenLine> StolenLines { get; set; }
        public DbSet<GPSObject> GPSObjects { get; set; }
        public DbSet<BatteryHistory> BatteryHistories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<DevicePositionHistory> DevicePositionHistories { get; set; }
        public DbSet<ObjectPositionHistory> ObjectPositionHistories { get; set; }
        public DbSet<Notification> Notifications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new GPSDeviceEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new StolenLineEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new BatteryHistoryEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new GPSObjectEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new CustomerEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new DevicePositionHistoryEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ObjectPositionHistoryEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new NotificationEntityTypeConfiguration());
        }
    }
}
