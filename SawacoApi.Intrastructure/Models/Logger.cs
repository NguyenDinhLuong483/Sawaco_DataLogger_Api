
namespace SawacoApi.Intrastructure.Models
{
    public class Logger
    {
        public string Id { get; set; }
        public double Longtitude { get; set; }
        public double Latitude { get; set; }
        public string Name { get; set; }
        public int Battery { get; set; }
        public string Temperature { get; set; }
        public bool Stolen { get; set; }
        public string Bluetooth { get; set; }
        public DateTime TimeStamp { get; set; }
        public List<StolenLine> StolenLines { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public Logger()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
        }

        public Logger(string id, double longtitude, double latitude, string name, int battery, string temp, bool stolen, string bluetooth, DateTime timeStamp, List<StolenLine> stolenLines)
        {
            Id = id;
            Longtitude = longtitude;
            Latitude = latitude;
            Name = name;
            Battery = battery;
            Temperature = temp;
            Stolen = stolen;
            Bluetooth = bluetooth;
            TimeStamp = timeStamp;
            StolenLines = stolenLines;
        }

        public void UpdateAll(double longtitude, double latitude, string name, int battery, string temp, bool stolen, string bluetooth, DateTime timeStamp)
        {
            Longtitude = longtitude;
            Latitude = latitude;
            Name = name;
            Battery = battery;
            Temperature = temp;
            Stolen = stolen;
            Bluetooth = bluetooth;
            TimeStamp = timeStamp;
        }
    }
}
