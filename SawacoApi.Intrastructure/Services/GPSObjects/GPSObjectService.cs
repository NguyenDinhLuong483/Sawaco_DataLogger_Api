
using SawacoApi.Intrastructure.Models;

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
                isExist = await _gPSObjectRepository.IsExistObjectid(viewModel.Id);
            }
            while (isExist);
            var newobject = _mapper.Map<CreateNewObjectViewModel, GPSObject>(viewModel);
            await _gPSObjectRepository.AddNewObject(newobject);
            await _unitOfWork.CompleteAsync();
            return newobject.Id;

        }

        public async Task<bool> SetupDeviceForObject(SetupDeviceViewModel viewModel)
        {
            var isExistDevice = await _gPSObjectRepository.IsExistDevice(DeviceId);
            var isExistObject = await _gPSObjectRepository.IsExistObjectid(ObjectId);
            if (isExistDevice && isExistObject)
            {
                var currentObject = await _gPSObjectRepository.FindObjectConnected(DeviceId);
                if (currentObject == null)
                {
                    var newObject = await _gPSObjectRepository.GetObjectByIdAsync(ObjectId);
                    newObject.Connected = true;
                    newObject.GPSDeviceId = DeviceId;
                    _gPSObjectRepository.UpdateObject(newObject);
                }
                else
                {
                    currentObject.Connected = false;
                    _gPSObjectRepository.UpdateObject(currentObject);

                    var newObject = await _gPSObjectRepository.GetObjectByIdAsync(ObjectId);
                    newObject.Connected = true;
                    newObject.GPSDeviceId = DeviceId;
                    _gPSObjectRepository.UpdateObject(newObject);
                }
                
                return await _unitOfWork.CompleteAsync();
            }
            else
            {
                return false;
            }
        }
    }
}
