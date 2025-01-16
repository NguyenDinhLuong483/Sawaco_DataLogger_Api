
namespace SawacoApi.Intrastructure.Repositories.GPSObjects
{
    public class GPSObjectRepository : BaseRepository, IGPSObjectRepository
    {
        public GPSObjectRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task AddNewObject(GPSObject gpsObject)
        {
            _context.GPSObjects.Add(gpsObject);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteObjectByIdAsync(GPSObject objectId)
        {
            _context.GPSObjects.Remove(objectId);
            await _context.SaveChangesAsync();
        }

        public async Task<GPSObject> FindObjectConnected(string deviceId)
        {
            return await _context.GPSObjects.FirstOrDefaultAsync(x => x.GPSDeviceId == deviceId && x.Connected);
        }

        public async Task<GPSDevice> GetDeviceByIdAsync(string id)
        {
            return await _context.GPSDevices.FirstOrDefaultAsync(x => x.Id == id) ?? throw new ResourceNotFoundException("Not found device id!");
        }

        public async Task<GPSObject> GetObjectByIdAsync(string id)
        {
            return await _context.GPSObjects.FindAsync(id) ?? throw new ResourceNotFoundException("Not found object id!");
        }

        public async Task<GPSObject> GetObjectByPhoneNumberAsync(string phoneNumber)
        {
            return await _context.GPSObjects.FirstOrDefaultAsync(x => x.CustomerPhoneNumber == phoneNumber) ?? throw new ResourceNotFoundException("Not found any object with this phone number!");
        }

        public async Task<bool> IsExistDevice(string id)
        {
            return await _context.GPSDevices.AnyAsync(x => x.Id == id);
        }

        public async Task<bool> IsExistObject(string id)
        {
            return await _context.GPSObjects.AnyAsync(x => x.Id == id);
        }

        public bool UpdateObject(GPSObject gpsObject)
        {
            _context.GPSObjects.Update(gpsObject);
            return true;
        }
    }
}
