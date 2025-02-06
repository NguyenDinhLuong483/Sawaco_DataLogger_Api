namespace SawacoApi.Intrastructure.Services.GPSDevices
{
    public interface IGPSDeviceService
    {
        public Task<List<GPSDeviceViewModel>> GetGPSDevice();
        public Task<GPSDeviceViewModel> GetGPSDeviceById(string id);
        public Task<bool> CreateNewGPSDevice(AddGPSDeviceViewModel addLogger);
        public Task<bool> DeleteGPSDevice(string loggerId);
        public Task<bool> UpdateGPSDeviceStatus(UpdateGPSDeviceViewModel updateDevice, string deviceId);
        public Task<bool> IsExistDevice(string deviceId);   
    }
}
