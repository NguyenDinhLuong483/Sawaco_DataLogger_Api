
namespace SawacoApi.Intrastructure.Repositories.GPSObjects
{
    public interface IGPSObjectRepository
    {
        public Task<GPSObject> GetObjectByIdAsync(string id);  
        public Task<GPSDevice> GetDeviceByIdAsync(string id);
        public Task AddNewObject(GPSObject gpsObject);
        public Task<bool> IsExistObject(string id);
        public Task<bool> IsExistDevice(string id);
        public Task<GPSObject> FindObjectConnected(string deviceId);
        public bool UpdateObject(GPSObject gpsObject);
        public Task<GPSObject> GetObjectByPhoneNumberAsync(string phoneNumber);
        public Task DeleteObjectByIdAsync(GPSObject objects);
    }
}
