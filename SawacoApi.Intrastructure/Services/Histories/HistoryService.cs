
namespace SawacoApi.Intrastructure.Services.Histories
{
    public class HistoryService : IHistoryService
    {
        public IGPSHistoryRepository _historyRepository { get; set; }
        public IMapper _mapper { get; set; }
        public IUnitOfWork _unitOfWork { get; set; }

        public HistoryService(IGPSHistoryRepository historyRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _historyRepository = historyRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> AddBatteryHistory(AddBatteryHistoryViewModel viewModel)
        {
            var source = _mapper.Map<AddBatteryHistoryViewModel, BatteryHistory>(viewModel);
            await _historyRepository.AddBatteryHistoryAsync(source);
            return await _unitOfWork.CompleteAsync();
        }

        public async Task<List<BatteryHistoryViewModel>> GetBatteryHistory(string DeviceId, DateTime startDate, DateTime endDate)
        {
            var source = await _historyRepository.GetBatteryHistoryAsync(DeviceId, startDate, endDate);
            var result = _mapper.Map<List<BatteryHistory>, List<BatteryHistoryViewModel>>(source);
            return result;
        }

        public async Task<bool> AddDevicePositionHistory(AddDevicePositionHistoryViewModel viewModel)
        {
            var source = _mapper.Map<AddDevicePositionHistoryViewModel, DevicePositionHistory>(viewModel);
            await _historyRepository.AddDevicePositionHistoryAsync(source);
            return await _unitOfWork.CompleteAsync();
        }

        public async Task<List<DevicePositionHistoryViewModel>> GetDevicePositionHistory(string DeviceId, DateTime startDate, DateTime endDate)
        {
            var source = await _historyRepository.GetDevicePositionHistoryAsync(DeviceId, startDate, endDate);
            var result = _mapper.Map<List<DevicePositionHistory>, List<DevicePositionHistoryViewModel>>(source);
            return result;
        }

        public async Task<bool> AddObjectPositionHistory(AddObjectPositionHistoryViewModel viewModel)
        {
            var source = _mapper.Map<AddObjectPositionHistoryViewModel, ObjectPositionHistory>(viewModel);
            await _historyRepository.AddObjectPositionHistoryAsync(source);
            return await _unitOfWork.CompleteAsync();
        }

        public async Task<List<ObjectPositionHistoryViewModel>> GetObjectPositionHistory(string ObjectId, DateTime startDate, DateTime endDate)
        {
            var source = await _historyRepository.GetObjectPositionHistoryAsync(ObjectId, startDate, endDate);
            var result = _mapper.Map<List<ObjectPositionHistory>, List<ObjectPositionHistoryViewModel>>(source);
            return result;
        }

        public async Task<bool> UpdateBatteryHistory(string id, DateTime startDate, DateTime endDate, int battery)
        {
            var source = await _historyRepository.GetBatteryHistoryAsync(id, startDate, endDate);
            if(source == null || source.Count == 0)
            {
                return false;
            }
            else
            {
                foreach (var item in source)
                {
                    item.Value = battery;
                }
                await _historyRepository.UpdateBatteryHistory(source);
                return await _unitOfWork.CompleteAsync();
            }
        }

        public Task<bool> DeleteBatteryHistory(string id, DateTime startDate, DateTime endDate)
        {
            _historyRepository.DeleteBatteryHistory(id, startDate, endDate);
            return _unitOfWork.CompleteAsync();
        }
    }
}
