
namespace SawacoApi.Intrastructure.Services.Histories
{
    public interface IHistoryService
    {
       public Task<bool> AddBatteryHistory(AddBatteryHistoryViewModel viewModel);
        public Task<bool> UpdateBatteryHistory(string id, DateTime startDate, DateTime endDate, int battery);
        public Task<bool> AddDevicePositionHistory(AddDevicePositionHistoryViewModel viewModel);
       public Task<bool> AddObjectPositionHistory(AddObjectPositionHistoryViewModel viewModel);
       public Task<List<BatteryHistoryViewModel>> GetBatteryHistory(string DeviceId, DateTime startDate, DateTime endDate);
       public Task<List<DevicePositionHistoryViewModel>> GetDevicePositionHistory(string DeviceId, DateTime startDate, DateTime endDate);
       public Task<List<ObjectPositionHistoryViewModel>> GetObjectPositionHistory(string ObjectId, DateTime startDate, DateTime endDate);
    }
}
