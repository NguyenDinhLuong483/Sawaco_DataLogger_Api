
namespace SawacoApi.Intrastructure.Repositories.Histories
{
    public class GPSHistoryRepository : BaseRepository, IGPSHistoryRepository
    {
        public GPSHistoryRepository(ApplicationDbContext context) : base(context)
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

        public bool DeleteBatteryHistory(string id, DateTime startDate, DateTime endDate)
        {
            _context.BatteryHistories.RemoveRange(_context.BatteryHistories.Where(x => x.GPSDeviceId == id && x.Timestamp >= startDate && x.Timestamp <= endDate));
            return _context.SaveChanges() > 0;
        }

        public async Task<List<BatteryHistory>> GetBatteryHistoryAsync(string DeviceId, DateTime startDate, DateTime endDate)
        {
            return await _context.BatteryHistories.Where(x => x.GPSDeviceId == DeviceId && x.Timestamp >= startDate && x.Timestamp <= endDate).ToListAsync();
        }

        public async Task<List<DevicePositionHistory>> GetDevicePositionHistoryAsync(string DeviceId, DateTime startDate, DateTime endDate)
        {
            return await _context.DevicePositionHistories.Where(x => x.GPSDeviceId == DeviceId && x.Timestamp >= startDate && x.Timestamp <= endDate).ToListAsync();
        }

        public async Task<List<ObjectPositionHistory>> GetObjectPositionHistoryAsync(string ObjectId, DateTime startDate, DateTime endDate)
        {
            return await _context.ObjectPositionHistories.Where(x => x.GPSObjectId == ObjectId && x.Timestamp >= startDate && x.Timestamp <= endDate).ToListAsync();
        }

        public async Task<bool> UpdateBatteryHistory(List<BatteryHistory> batteryHistory)
        {
            _context.BatteryHistories.UpdateRange(batteryHistory);
            return await _context.SaveChangesAsync() > 0;

        }
    }
}
