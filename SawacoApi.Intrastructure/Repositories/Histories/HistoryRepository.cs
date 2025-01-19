
namespace SawacoApi.Intrastructure.Repositories.Histories
{
    public class HistoryRepository : BaseRepository, IHistoryRepository
    {
        public HistoryRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task AddBatteryHistoryAsync(BatteryHistory batteryHistory)
        {
            await _context.BatteryHistories.AddAsync(batteryHistory);
        }

        public async Task AddDevicePositionHistoryAsync(DevicePositionHistory devicePositionHistory)
        {
            await _context.DevicePositionHistories.AddAsync(devicePositionHistory);
        }

        public async Task AddObjectPositionHistoryAsync(ObjectPositionHistory devicePositionHistory)
        {
            await _context.ObjectPositionHistories.AddAsync(devicePositionHistory);
        }

        public async Task<List<BatteryHistory>> GetBatteryHistoryAsync(DateTime startDate, DateTime endDate)
        {
            return await _context.BatteryHistories.Where(x => x.Timestamp >= startDate && x.Timestamp <= endDate).ToListAsync();
        }

        public async Task<List<DevicePositionHistory>> GetDevicePositionHistoryAsync(DateTime startDate, DateTime endDate)
        {
            return await _context.DevicePositionHistories.Where(x => x.Timestamp >= startDate && x.Timestamp <= endDate).ToListAsync();
        }

        public async Task<List<ObjectPositionHistory>> GetObjectPositionHistoryAsync(DateTime startDate, DateTime endDate)
        {
            return await _context.ObjectPositionHistories.Where(x => x.Timestamp >= startDate && x.Timestamp <= endDate).ToListAsync();
        }
    }
}
