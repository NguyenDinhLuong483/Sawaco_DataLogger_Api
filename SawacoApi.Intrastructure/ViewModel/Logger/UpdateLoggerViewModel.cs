using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SawacoApi.Intrastructure.ViewModel.Logger
{
    public class UpdateLoggerViewModel
    {
        public double Longtitude { get; set; }
        public double Latitude { get; set; }
        public string Name { get; set; }
        public int Battery { get; set; }
        public string Temperature { get; set; }
        public bool Stolen { get; set; }
        public string Bluetooth { get; set; }
        public DateTime TimeStamp { get; set; }

        public UpdateLoggerViewModel(double longtitude, double latitude, string name, int battery, string temperature, bool stolen, string bluetooth, DateTime timeStamp)
        {
            Longtitude = longtitude;
            Latitude = latitude;
            Name = name;
            Battery = battery;
            Temperature = temperature;
            Stolen = stolen;
            Bluetooth = bluetooth;
            TimeStamp = timeStamp;
        }
    }
}
