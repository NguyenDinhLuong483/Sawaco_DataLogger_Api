namespace SawacoApi.Intrastructure.Repositories.GPSDevices
{
    public class GPSDeviceRepository : BaseRepository, IGPSDeviceRepository
    {
        public GPSDeviceRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<List<GPSDevice>> GetAllDeviceAsync()
        {
            return await _context.GPSDevices.Include(x => x.StolenLines).ToListAsync();
        }

        public async Task<GPSDevice> GetDeviceByIdAsync(string id)
        {
            return await _context.GPSDevices.Include(x => x.StolenLines).FirstOrDefaultAsync(x => x.Id == id) ?? throw new ResourceNotFoundException("Not found device!");
        }

        public async Task<GPSDevice> FindDevice(string id)
        {
            return await _context.GPSDevices.FindAsync(id) ?? throw new ResourceNotFoundException("Not found this device!");
        }

        public GPSDevice CreateDeviceAsync(GPSDevice newDevice)
        {
            if (newDevice.Id == "")
            {
                throw new ResourceNotFoundException("Impossible create this id!");
            }
            else
            {
                return _context.GPSDevices.Add(newDevice).Entity;
            }
        }

        public bool DeleteDeviceAsync(GPSDevice deleteDevice)
        {
            _context.GPSDevices.RemoveRange(deleteDevice);
            return true;
        }

        public bool UpdateDeviceAsync(GPSDevice updateDevice)
        {
            _context.Update(updateDevice);
            return true;
        }

        public async Task<bool> IsExistDevice(string id)
        {
            return await _context.GPSDevices.AnyAsync(x => x.Id == id);
        }
    }
}
