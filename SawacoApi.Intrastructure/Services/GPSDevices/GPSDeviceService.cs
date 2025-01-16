
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

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
                if (updateDevice.Longitude != 0)
                {
                    device.Longitude = updateDevice.Longitude;
                }
                if (updateDevice.Latitude != 0)
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
                if (updateDevice.Battery != 0)
                {
                    device.Battery = updateDevice.Battery;
                }
                if (updateDevice.Temperature != 0)
                {
                    device.Temperature = updateDevice.Temperature;
                }
                if (!string.IsNullOrEmpty(updateDevice.Stolen.ToString())) // Assuming Stolen is boolean and default is false
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
                _GPSDeviceRepository.UpdateDeviceAsync(device);
                return await _unitOfWork.CompleteAsync();
            }
            else
            {
                return false;
            }
        }
    }
}
