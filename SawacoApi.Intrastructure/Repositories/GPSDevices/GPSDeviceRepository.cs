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

        public async Task<GPSDevice> CreateDeviceAsync(GPSDevice newDevice)
        {
            if (newDevice.Id == "")
            {
                throw new ResourceNotFoundException("Impossible create this id!");
            }
            else
            {
                var entity = await _context.GPSDevices.AddAsync(newDevice);
                return entity.Entity;
            }
        }

        public bool DeleteDeviceAsync(GPSDevice deleteDevice)
        {
            _context.GPSDevices.RemoveRange(deleteDevice);
            return true;
        }

        public async Task UpdateDeviceAsync(GPSDevice updateDevice)
        {
            _context.Update(updateDevice);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> IsExistDevice(string id)
        {
            return await _context.GPSDevices.AnyAsync(x => x.Id == id);
        }
    }
}
