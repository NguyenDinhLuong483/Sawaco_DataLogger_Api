
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

        public async Task<GPSObject> FindObjectConnected(string deviceId)
        {
            return await _context.GPSObjects.FirstOrDefaultAsync(x => x.GPSDeviceId == deviceId && x.Connected);
        }

        public async Task<GPSObject> GetObjectByIdAsync(string id)
        {
            return await _context.GPSObjects.FindAsync(id) ?? throw new ResourceNotFoundException("Not found object id!");
        }

        public async Task<bool> IsExistDevice(string id)
        {
            return await _context.GPSDevices.AnyAsync(x => x.Id == id);
        }

        public async Task<bool> IsExistObjectid(string id)
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
