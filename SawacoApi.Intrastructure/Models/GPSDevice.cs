
namespace SawacoApi.Intrastructure.Models
{
    public class GPSDevice
    {
        public string Id { get; set; }
        public string CustomerPhoneNumber {  get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string Name { get; set; }
        public string ImagePath {  get; set; }
        public double Battery { get; set; }
        public double Temperature { get; set; }
        public bool Stolen { get; set; }
        public string Bluetooth { get; set; }
        public DateTime TimeStamp { get; set; }
        public string SMSNumber {  get; set; }
        public string Package { get; set; }
        public DateTime RegistationDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public Customer Customer { get; set; }
        public List<StolenLine> StolenLines { get; set; }
        public List<DevicePositionHistory> DevicePositionHistories { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        public GPSDevice()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        {
        }

        public GPSDevice(string id, string customerPhoneNumber, double longitude, double latitude, string name, string imagePath, double battery, double temperature, bool stolen, string bluetooth, DateTime timeStamp, string sMSNumber, string package, DateTime registationDate, DateTime expirationDate)
        {
            Id = id;
            CustomerPhoneNumber = customerPhoneNumber;
            Longitude = longitude;
            Latitude = latitude;
            Name = name;
            ImagePath = imagePath;
            Battery = battery;
            Temperature = temperature;
            Stolen = stolen;
            Bluetooth = bluetooth;
            TimeStamp = timeStamp;
            SMSNumber = sMSNumber;
            Package = package;
            RegistationDate = registationDate;
            ExpirationDate = expirationDate;
        }
    }
}
