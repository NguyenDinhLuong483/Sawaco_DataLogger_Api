﻿
namespace SawacoApi.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FirmwareController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private IWebHostEnvironment _env;

        public FirmwareController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // Upload firmware mới
        [HttpPost("upload/version={version}")]
        public async Task<IActionResult> UploadFirmware(List<IFormFile> files, [FromRoute] string version)
        {
            if (files == null)
            {
                return BadRequest("No files uploaded.");
            }

            // Kiểm tra xem phiên bản đã tồn tại chưa
            var existingFirmware = await _context.Firmwares.FirstOrDefaultAsync(f => f.Version == version);
            if (existingFirmware != null)
            {
                return BadRequest("Version already exists.");
            }

            foreach (var formFile in files)
            {
                // Lưu files vào thư mục wwwroot/firmware
                var uploadDir = Path.Combine(_env.WebRootPath, $"firmwareVersion{version}");
                Directory.CreateDirectory(uploadDir);

                var filePath = Path.Combine(uploadDir, formFile.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await formFile.CopyToAsync(stream);
                }
                // Lưu thông tin vào database
                var firmware = new Firmware
                {
                    Version = version,
                    ReleaseDate = DateTime.UtcNow.AddHours(7),
                    FilePath = Path.Combine("firmware", formFile.FileName) // Lưu đường dẫn tương đối
                };

                _context.Firmwares.Add(firmware);
                await _context.SaveChangesAsync();
            }
            return Ok(new { message = "Firmware uploaded successfully!", version });
        }

        // Lấy phiên bản firmware mới nhất
        [HttpGet("version")]
        public async Task<IActionResult> GetLatestVersion()
        {
            var latestFirmware = await _context.Firmwares
                .OrderByDescending(f => f.ReleaseDate)
                .FirstOrDefaultAsync();

            if (latestFirmware == null)
            {
                return NotFound("No firmware available.");
            }

            return Ok(new { version = latestFirmware.Version });
        }

        // Kiểm tra cập nhật từ ESP32
        [HttpGet("checkUpdate/currentVersion={currentVersion}")]
        public async Task<IActionResult> CheckForUpdate(string currentVersion)
        {
            var latestFirmware = await _context.Firmwares
                .OrderByDescending(f => f.ReleaseDate)
                .FirstOrDefaultAsync();

            if (latestFirmware == null)
            {
                return NotFound("No firmware available.");
            }

            if (latestFirmware.Version == currentVersion)
            {
                return Ok(new { updateAvailable = false });
            }

            return Ok(new
            {
                updateAvailable = true,
                version = latestFirmware.Version,
                downloadUrl = Url.Action(nameof(DownloadFirmware), "Firmware", new { version = latestFirmware.Version }, Request.Scheme)
            });
        }

        // Tải firmware theo phiên bản
        [HttpGet("download/version={version}")]
        public async Task<IActionResult> DownloadFirmware(string version)
        {
            var firmware = await _context.Firmwares
                .FirstOrDefaultAsync(f => f.Version == version);

            if (firmware == null)
            {
                return NotFound("Firmware version not found.");
            }

            var filePath = Path.Combine(_env.WebRootPath, firmware.FilePath);
            if (!System.IO.File.Exists(filePath))
            {
                return NotFound("Firmware files not found.");
            }

            var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            return File(fileStream, "application/octet-stream", Path.GetFileName(filePath));
        }
    }
}
