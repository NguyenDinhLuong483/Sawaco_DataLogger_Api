namespace SawacoApi.Intrastructure.Repositories.GPSDevices
{
    public interface IGPSDeviceRepository
    {
        public Task<List<GPSDevice>> GetAllDeviceAsync();
        public Task<GPSDevice> GetDeviceByIdAsync(string id);
        public Task<GPSDevice> CreateDeviceAsync(GPSDevice device);
        public bool DeleteDeviceAsync(GPSDevice device);
        public Task UpdateDeviceAsync(GPSDevice device);
        public Task<bool> IsExistDevice(string id);
    }
}
