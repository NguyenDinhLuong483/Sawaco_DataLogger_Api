
namespace SawacoApi.Intrastructure.Services.Histories
{
    public interface IHistoryService
    {
       public Task<bool> AddBatteryHistory(AddBatteryHistoryViewModel viewModel);
       public Task<bool> AddDevicePositionHistory(AddDevicePositionHistoryViewModel viewModel);
       public Task<bool> AddObjectPositionHistory(AddObjectPositionHistoryViewModel viewModel);
       public Task<List<BatteryHistoryViewModel>> GetBatteryHistory(DateTime startDate, DateTime endDate);
       public Task<List<DevicePositionHistoryViewModel>> GetDevicePositionHistory(DateTime startDate, DateTime endDate);
       public Task<List<ObjectPositionHistoryViewModel>> GetObjectPositionHistory(DateTime startDate, DateTime endDate);
    }
}
