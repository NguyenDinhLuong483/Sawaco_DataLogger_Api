namespace SawacoApi.Intrastructure.Repositories.GPSDevices
{
    public interface IGPSDeviceRepository
    {
        public Task<List<GPSDevice>> GetAllDeviceAsync();
        public Task<GPSDevice> GetDeviceByIdAsync(string id);
        public Task<GPSDevice> FindDevice(string id);
        public GPSDevice CreateDeviceAsync(GPSDevice newlogger);
        public bool DeleteDeviceAsync(GPSDevice deleteLogger);
        public bool UpdateDeviceAsync(GPSDevice newlogger);
    }
}
