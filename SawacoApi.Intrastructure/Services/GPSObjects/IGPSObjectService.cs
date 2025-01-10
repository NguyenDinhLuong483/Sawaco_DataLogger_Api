﻿
namespace SawacoApi.Intrastructure.Services.GPSObjects
{
    public interface IGPSObjectService
    {
        public Task<GPSObjectViewModel> GetObjectById(string id);
        public Task<string> CreateNewObject(CreateNewObjectViewModel viewModel);
        public Task<bool> SetupDeviceForObject(SetupDeviceViewModel viewModel);
    }
}
