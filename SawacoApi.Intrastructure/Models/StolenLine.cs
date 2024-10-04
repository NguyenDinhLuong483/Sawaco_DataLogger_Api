
namespace SawacoApi.Intrastructure.Models
{
    public class StolenLine
    {
        public int Id { get; set; }
        public string LoggerId { get; set; }
        public double Longtitude { get; set; }
        public double Latitude { get; set; }
        public int Battery { get; set; }
        public DateTime TimeStamp { get; set; }
        public Logger Logger { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public StolenLine()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
        }

        public StolenLine(string loggerId, double longtitude, double latitude, int battery, DateTime timeStamp)
        {
            LoggerId = loggerId;
            Longtitude = longtitude;
            Latitude = latitude;
            TimeStamp = timeStamp;
            Battery = battery;
        }
    }
}
