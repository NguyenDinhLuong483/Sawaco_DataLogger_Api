
namespace SawacoApi.Intrastructure.Models
{
    public class Firmware
    {
        public int Id { get; set; }
        public string Version { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string FilePath { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        public Firmware()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        {
        }

        public Firmware(int id, string version, DateTime releaseDate, string filePath)
        {
            Id = id;
            Version = version;
            ReleaseDate = releaseDate;
            FilePath = filePath;
        }
    }
}
