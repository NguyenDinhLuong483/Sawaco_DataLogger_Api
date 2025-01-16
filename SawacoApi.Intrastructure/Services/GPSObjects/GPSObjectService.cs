
namespace SawacoApi.Intrastructure.Services.GPSObjects
{
    public class GPSObjectService : IGPSObjectService
    {
        public IGPSObjectRepository _gPSObjectRepository { get; set; }
        public IUnitOfWork _unitOfWork { get; set; }
        public IMapper _mapper { get; set; }

        public GPSObjectService(IGPSObjectRepository gPSObjectRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _gPSObjectRepository = gPSObjectRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GPSObjectViewModel> GetObjectById(string id)
        {
            var source = await _gPSObjectRepository.GetObjectByIdAsync(id);
            var resource = _mapper.Map<GPSObject, GPSObjectViewModel>(source);
            return resource;
        }

        public async Task<string> CreateNewObject(CreateNewObjectViewModel viewModel)
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
                        _gPSObjectRepository.UpdateObject(newObject);
                    }
                    else
                    {
                        currentObject.Connected = false;
                        _gPSObjectRepository.UpdateObject(currentObject);

                        var newObject = await _gPSObjectRepository.GetObjectByIdAsync(viewModel.ObjectId);
                        newObject.Connected = true;
                        newObject.GPSDeviceId = viewModel.DeviceId;
                        _gPSObjectRepository.UpdateObject(newObject);
                    }
                    return await _unitOfWork.CompleteAsync();
                }
            }
            else
            {
                return false;
            }
        }

        public async Task<GPSObjectViewModel> GetObjectByPhoneNumber(string phoneNumber)
        {
            var source = await _gPSObjectRepository.GetObjectByPhoneNumberAsync(phoneNumber);
            var result = _mapper.Map<GPSObject, GPSObjectViewModel>(source);
            return result;
        }

        public async Task<bool> DeleteObjectById(string Id)
        {
            var objects = await _gPSObjectRepository.GetObjectByIdAsync(Id);
            await _gPSObjectRepository.DeleteObjectByIdAsync(objects);
            return await _unitOfWork.CompleteAsync();
        }
    }
}
