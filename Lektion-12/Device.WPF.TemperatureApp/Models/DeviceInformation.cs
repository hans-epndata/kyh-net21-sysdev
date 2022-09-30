using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Device.WPF.TemperatureApp.Models
{
    public class DeviceInformation
    {
        public string DeviceId { get; } = Guid.NewGuid().ToString();
        public string DeviceType { get; } = "thermometer";
        public string DeviceName { get; set; } = "";
        public string Location { get; set; } = "";
        public string ConnectionString { get; set; } = "";
        public bool IsConfigured { get; set; } = false;
    }
}
