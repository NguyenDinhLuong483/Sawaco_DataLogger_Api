
namespace SawacoApi.Intrastructure.Services.GPSDevices
{
    public class GPSDeviceService : IGPSDeviceService
    {
        public IGPSDeviceRepository _GPSDeviceRepository { get; set; }
        public IMapper _mapper { get; set; }
        public IUnitOfWork _unitOfWork { get; set; }

        public GPSDeviceService(IGPSDeviceRepository loggerRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _GPSDeviceRepository = loggerRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<GPSDeviceViewModel>> GetGPSDevice()
        {
            var source = await _GPSDeviceRepository.GetAllDeviceAsync() ?? throw new ResourceNotFoundException();
            var viewmodel = _mapper.Map<List<GPSDevice>, List<GPSDeviceViewModel>>(source);
            return viewmodel;
        }

        public async Task<GPSDeviceViewModel> GetGPSDeviceById(string id)
        {
            var source = await _GPSDeviceRepository.GetDeviceByIdAsync(id);
            var viewmodel = _mapper.Map<GPSDevice, GPSDeviceViewModel>(source);
            return viewmodel;
        }

        public async Task<bool> CreateNewGPSDevice(AddGPSDeviceViewModel addDevice)
        {
            var mapping = _mapper.Map<AddGPSDeviceViewModel, GPSDevice>(addDevice);
            var IsExist = await _GPSDeviceRepository.FindDevice(mapping.Id);
            if (IsExist is not null)
            {
                throw new EntityDuplicationException("This device is existing!");
            }
            var newdevice = _GPSDeviceRepository.CreateDeviceAsync(mapping);
            return await _unitOfWork.CompleteAsync();
        }

        public async Task<bool> DeleteGPSDevice(string loggerId)
        {
            var IsExist = await _GPSDeviceRepository.GetDeviceByIdAsync(loggerId) ?? throw new ResourceNotFoundException("Not found this Device!");
            _GPSDeviceRepository.DeleteDeviceAsync(IsExist);
            return await _unitOfWork.CompleteAsync();
        }

        public async Task<bool> UpdateGPSDeviceStatus(UpdateGPSDeviceViewModel updateDevice, string deviceId)
        {
            var IsExist = await _GPSDeviceRepository.GetDeviceByIdAsync(deviceId) ?? throw new ResourceNotFoundException();
            IsExist.UpdateAll(updateDevice.Longitude, updateDevice.Latitude, updateDevice.Name, updateDevice.Battery, updateDevice.Temperature, updateDevice.Stolen, updateDevice.Bluetooth, updateDevice.TimeStamp);
            var update = _GPSDeviceRepository.UpdateDeviceAsync(IsExist);
            return await _unitOfWork.CompleteAsync();
        }
    }
}
