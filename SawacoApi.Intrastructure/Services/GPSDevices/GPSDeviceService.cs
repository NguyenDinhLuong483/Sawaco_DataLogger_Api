
namespace SawacoApi.Intrastructure.Services.GPSDevices
{
    public class GPSDeviceService : IGPSDeviceService
    {
        public IGPSDeviceRepository _GPSDeviceRepository { get; set; }
        public IMapper _mapper { get; set; }
        public IUnitOfWork _unitOfWork { get; set; }
        public ManagedMqttClient _mqttClient { get; set; }

        public GPSDeviceService(IGPSDeviceRepository loggerRepository, IMapper mapper, IUnitOfWork unitOfWork, ManagedMqttClient mqttClient)
        {
            _GPSDeviceRepository = loggerRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _mqttClient = mqttClient;
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
            var IsExist = await _GPSDeviceRepository.IsExistDevice(addDevice.Id);
            if (IsExist)
            {
                throw new EntityDuplicationException("This device is existing!");
            }
            var mapping = _mapper.Map<AddGPSDeviceViewModel, GPSDevice>(addDevice);
            var newdevice = _GPSDeviceRepository.CreateDeviceAsync(mapping);
            return await _unitOfWork.CompleteAsync();
        }

        public async Task<bool> DeleteGPSDevice(string deviceId)
        {
            var IsExist = await _GPSDeviceRepository.GetDeviceByIdAsync(deviceId);
            _GPSDeviceRepository.DeleteDeviceAsync(IsExist);
            return await _unitOfWork.CompleteAsync();
        }

        public async Task<bool> UpdateGPSDeviceStatus(UpdateGPSDeviceViewModel updateDevice, string deviceId)
        {
            var isExist = await _GPSDeviceRepository.IsExistDevice(deviceId);
            if (isExist)
            {
                var device = await _GPSDeviceRepository.GetDeviceByIdAsync(deviceId);
                if (!string.IsNullOrEmpty(updateDevice.CustomerPhoneNumber))
                {
                    device.CustomerPhoneNumber = updateDevice.CustomerPhoneNumber;
                }
                if (!string.IsNullOrEmpty(updateDevice.Longitude.ToString()))
                {
                    device.Longitude = updateDevice.Longitude;
                }
                if (!string.IsNullOrEmpty(updateDevice.Latitude.ToString()))
                {
                    device.Latitude = updateDevice.Latitude;
                }
                if (!string.IsNullOrEmpty(updateDevice.Name))
                {
                    device.Name = updateDevice.Name;
                }
                if (!string.IsNullOrEmpty(updateDevice.ImagePath))
                {
                    device.ImagePath = updateDevice.ImagePath;
                }
                if (!string.IsNullOrEmpty(updateDevice.Battery.ToString()))
                {
                    device.Battery = updateDevice.Battery;
                }
                if (!string.IsNullOrEmpty(updateDevice.Temperature.ToString()))
                {
                    device.Temperature = updateDevice.Temperature;
                }
                if (!string.IsNullOrEmpty(updateDevice.Stolen.ToString()))
                {
                    device.Stolen = updateDevice.Stolen;
                }
                if (!string.IsNullOrEmpty(updateDevice.Bluetooth))
                {
                    device.Bluetooth = updateDevice.Bluetooth;
                }
                if (!string.IsNullOrEmpty(updateDevice.TimeStamp.ToString()))
                {
                    device.TimeStamp = updateDevice.TimeStamp;
                }
                if (!string.IsNullOrEmpty(updateDevice.SMSNumber))
                {
                    device.SMSNumber = updateDevice.SMSNumber;
                }
                if (!string.IsNullOrEmpty(updateDevice.Package))
                {
                    device.Package = updateDevice.Package;
                }
                if (!string.IsNullOrEmpty(updateDevice.RegistationDate.ToString()))
                {
                    device.RegistationDate = updateDevice.RegistationDate;
                }
                if (!string.IsNullOrEmpty(updateDevice.ExpirationDate.ToString()))
                {
                    device.ExpirationDate = updateDevice.ExpirationDate;
                }
                await _GPSDeviceRepository.UpdateDeviceAsync(device);
                return await _unitOfWork.CompleteAsync();
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> IsExistDevice(string deviceId)
        {
            return await _GPSDeviceRepository.IsExistDevice(deviceId);
        }
    }
}
