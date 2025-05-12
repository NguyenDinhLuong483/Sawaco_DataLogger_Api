
namespace SawacoApi.Intrastructure.Repositories.Histories
{
    public interface IGPSHistoryRepository
    {
        public Task AddBatteryHistoryAsync(BatteryHistory batteryHistory);
        public Task<bool> UpdateBatteryHistory(List<BatteryHistory> batteryHistory);
        public Task AddDevicePositionHistoryAsync(DevicePositionHistory devicePositionHistory);
        public Task AddObjectPositionHistoryAsync(ObjectPositionHistory devicePositionHistory);
        public Task<List<BatteryHistory>> GetBatteryHistoryAsync(string DeviceId, DateTime startDate, DateTime endDate);
        public Task<List<DevicePositionHistory>> GetDevicePositionHistoryAsync(string DeviceId, DateTime startDate, DateTime endDate);
        public Task<List<ObjectPositionHistory>> GetObjectPositionHistoryAsync(string ObjectId, DateTime startDate, DateTime endDate);
        public bool DeleteBatteryHistory(string id, DateTime startDate, DateTime endDate);
    }
}
