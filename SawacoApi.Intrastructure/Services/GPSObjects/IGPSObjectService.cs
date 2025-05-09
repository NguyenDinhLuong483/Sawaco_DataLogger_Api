﻿
namespace SawacoApi.Intrastructure.Services.GPSObjects
{
    public interface IGPSObjectService
    {
        public Task<GPSObjectViewModel> GetObjectById(string id);
        public Task<string> CreateNewObject(CreateNewObjectViewModel viewModel);
        public Task<bool> SetupDeviceForObject(SetupDeviceViewModel viewModel);
        public Task<List<GPSObjectViewModel>> GetObjectByPhoneNumber(string phoneNumber);
        public Task<bool> DeleteObjectById(string Id);
        public Task<bool> UpdateObjectInformation(UpdateGPSObjectViewModel viewModel, string id);
        public Task<bool> CancelConnection(string id);
        public Task<GPSObject> FindObjectConnected(string deviceId);
    }
}
