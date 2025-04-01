
using Newtonsoft.Json;

namespace SawacoApi.Intrastructure.Services.GPSObjects
{
    public class GPSObjectService : IGPSObjectService
    {
        public IGPSObjectRepository _gPSObjectRepository { get; set; }
        public IUnitOfWork _unitOfWork { get; set; }
        public IMapper _mapper { get; set; }
        public ManagedMqttClient _mqttClient;

        public GPSObjectService(IGPSObjectRepository gPSObjectRepository, IUnitOfWork unitOfWork, IMapper mapper, ManagedMqttClient mqttClient)
        {
            _gPSObjectRepository = gPSObjectRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _mqttClient = mqttClient;
        }

        public async Task<GPSObjectViewModel> GetObjectById(string id)
        {
            var source = await _gPSObjectRepository.GetObjectByIdAsync(id);
            var resource = _mapper.Map<GPSObject, GPSObjectViewModel>(source);
            return resource;
        }

        public async Task<string> CreateNewObject(CreateNewObjectViewModel viewModel)
        {
            var customer = await _gPSObjectRepository.IsExistPhoneNumber(viewModel.CustomerPhoneNumber);
            if (customer)
            {
                bool isExist;
                do
                {
                    viewModel.Id = new Random().Next(100000, 999999).ToString();
                    isExist = await _gPSObjectRepository.IsExistObject(viewModel.Id);
                }
                while (isExist);
                var newobject = _mapper.Map<CreateNewObjectViewModel, GPSObject>(viewModel);
                await _gPSObjectRepository.AddNewObject(newobject);
                await _unitOfWork.CompleteAsync();
                return newobject.Id;
            }
            else
            {
                return "Phone Number is not correct!";
            }

        }

        public async Task<bool> SetupDeviceForObject(SetupDeviceViewModel viewModel)
        {
            var isExistDevice = await _gPSObjectRepository.IsExistDevice(viewModel.DeviceId);
            var isExistObject = await _gPSObjectRepository.IsExistObject(viewModel.ObjectId);
            if (isExistDevice && isExistObject)
            {
                var checkDevice = await _gPSObjectRepository.GetDeviceByIdAsync(viewModel.DeviceId);
                var checkObject = await _gPSObjectRepository.GetObjectByIdAsync(viewModel.ObjectId);
                if(viewModel.PhoneNumber != checkDevice.CustomerPhoneNumber || viewModel.PhoneNumber != checkObject.CustomerPhoneNumber)
                {
                    return false;
                }
                else
                {
                    var currentObject = await _gPSObjectRepository.FindObjectConnected(viewModel.DeviceId);
                    if (currentObject == null)
                    {
                        var newObject = await _gPSObjectRepository.GetObjectByIdAsync(viewModel.ObjectId);
                        newObject.Connected = true;
                        newObject.GPSDeviceId = viewModel.DeviceId;
                        await _gPSObjectRepository.UpdateObject(newObject);
                    }
                    else
                    {
                        currentObject.Connected = false;
                        await _gPSObjectRepository.UpdateObject(currentObject);

                        var newObject = await _gPSObjectRepository.GetObjectByIdAsync(viewModel.ObjectId);
                        newObject.Connected = true;
                        newObject.GPSDeviceId = viewModel.DeviceId;
                        await _gPSObjectRepository.UpdateObject(newObject);
                    }
                    return await _unitOfWork.CompleteAsync();
                }
            }
            else
            {
                return false;
            }
        }

        public async Task<List<GPSObjectViewModel>> GetObjectByPhoneNumber(string phoneNumber)
        {
            var source = await _gPSObjectRepository.GetObjectByPhoneNumberAsync(phoneNumber);
            var result = _mapper.Map<List<GPSObject>, List<GPSObjectViewModel>>(source);
            return result;
        }

        public async Task<bool> DeleteObjectById(string Id)
        {
            var objects = await _gPSObjectRepository.GetObjectByIdAsync(Id);
            await _gPSObjectRepository.DeleteObjectByIdAsync(objects);
            return await _unitOfWork.CompleteAsync();
        }

        public async Task<bool> UpdateObjectInformation(UpdateGPSObjectViewModel viewModel, string id)
        {
            var isExist = await _gPSObjectRepository.IsExistObject(id);
            if (isExist) 
            {
                
                var data = new MqttSettingObject();
                var updateObject = await _gPSObjectRepository.GetObjectByIdAsync(id);
                var updateDevice = await _gPSObjectRepository.GetDeviceByIdAsync(updateObject.GPSDeviceId);
                var topic = $"GPS/Setting/{updateObject.GPSDeviceId}";
                if (!string.IsNullOrEmpty(viewModel.CustomerPhoneNumber))
                {
                    updateObject.CustomerPhoneNumber = viewModel.CustomerPhoneNumber;
                    data.PhoneNumber = viewModel.CustomerPhoneNumber;
                }
                else
                {
                    data.PhoneNumber = updateObject.CustomerPhoneNumber;
                }

                if (!string.IsNullOrEmpty(viewModel.Longitude.ToString()))
                {
                    updateObject.Longitude = viewModel.Longitude;
                    data.Longitude = viewModel.Longitude;
                }
                else
                {
                    data.Longitude = updateObject.Longitude;
                }

                if (!string.IsNullOrEmpty(viewModel.Latitude.ToString()))
                {
                    updateObject.Latitude = viewModel.Latitude;
                    data.Latitude = viewModel.Latitude;
                }
                else
                {
                    data.Latitude = updateObject.Latitude;
                }

                if (!string.IsNullOrEmpty(viewModel.Name))
                {
                    updateObject.Name = viewModel.Name;
                }
                if (!string.IsNullOrEmpty(viewModel.ImagePath))
                {
                    updateObject.ImagePath = viewModel.ImagePath;
                }
                if (!string.IsNullOrEmpty(viewModel.Description))
                {
                    updateObject.Description = viewModel.Description;
                }
                if (!string.IsNullOrEmpty(viewModel.SafeRadius.ToString()))
                {
                    updateObject.SafeRadius = viewModel.SafeRadius;
                    data.SafeRadius = viewModel.SafeRadius;
                }
                else
                {
                    data.SafeRadius = updateObject.SafeRadius;
                }
                if (!string.IsNullOrEmpty(viewModel.Size))
                {
                    updateObject.Size = viewModel.Size;
                }
                if (!string.IsNullOrEmpty(viewModel.CurrentTime.ToString()))
                {
                    data.CurrentTime = viewModel.CurrentTime;
                }
                if (!string.IsNullOrEmpty(viewModel.AlarmTime.ToString()))
                {
                    data.AlarmTime = viewModel.AlarmTime;
                    updateDevice.AlarmTime = viewModel.AlarmTime;
                }
                if (!string.IsNullOrEmpty(viewModel.BlueTooth))
                {
                    data.BlueTooth = viewModel.BlueTooth;
                }
                if (!string.IsNullOrEmpty(viewModel.Buzzer))
                {
                    data.Buzzer = viewModel.Buzzer;
                }
                if (!string.IsNullOrEmpty(viewModel.Emergency.ToString()))
                {
                    data.Emergency = viewModel.Emergency;
                    updateDevice.Emergency = viewModel.Emergency;
                }

                var message = JsonConvert.SerializeObject(data);
                if (updateObject.Connected)
                {
                    await _mqttClient.Publish(topic, message, true);
                }
                
                await _gPSObjectRepository.UpdateObject(updateObject);
                await _gPSObjectRepository.UpdateDevice(updateDevice);
                return await _unitOfWork.CompleteAsync();
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> CancelConnection(string id)
        {
            var isExist = await _gPSObjectRepository.IsExistObject(id);
            if (isExist)
            {
                var updateObject = await _gPSObjectRepository.GetObjectByIdAsync(id);
                updateObject.Connected = false;
                await _gPSObjectRepository.UpdateObject(updateObject);
                return await _unitOfWork.CompleteAsync();
            }
            else
            {
                return false;
            }
        }

        public async Task<GPSObject> FindObjectConnected(string deviceId)
        {
            return await _gPSObjectRepository.FindObjectConnected(deviceId);
        }
    }
}
