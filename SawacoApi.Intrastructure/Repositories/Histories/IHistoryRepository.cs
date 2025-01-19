
namespace SawacoApi.Intrastructure.Repositories.Histories
{
    public interface IHistoryRepository
    {
        public Task AddBatteryHistoryAsync(BatteryHistory batteryHistory);
        public Task AddDevicePositionHistoryAsync(DevicePositionHistory devicePositionHistory);
        public Task AddObjectPositionHistoryAsync(ObjectPositionHistory devicePositionHistory);
        public Task<List<BatteryHistory>> GetBatteryHistoryAsync(DateTime startDate, DateTime endDate);
        public Task<List<DevicePositionHistory>> GetDevicePositionHistoryAsync(DateTime startDate, DateTime endDate);
        public Task<List<ObjectPositionHistory>> GetObjectPositionHistoryAsync(DateTime startDate, DateTime endDate);
    }
}
