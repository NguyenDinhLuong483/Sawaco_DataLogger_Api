
namespace SawacoApi.Intrastructure.Repositories.GPSObjects
{
    public interface IGPSObjectRepository
    {
        public Task<GPSObject> GetObjectByIdAsync(string id);  
        public Task AddNewObject(GPSObject gpsObject);
        public Task<bool> IsExistObjectid(string id);
        public Task<bool> IsExistDevice(string id);
        public Task<GPSObject> FindObjectConnected(string deviceId);
        public bool UpdateObject(GPSObject gpsObject);
    }
}
