using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Device.ConsoleApp.Models
{
    internal class DeviceSettings
    {
        public string DeviceId { get; set; } = "";
        public string ConnectionString { get; set; } = "";
        public string DeviceName { get; set; } = "";
        public string DeviceType { get; set; } = "";
        public string Location { get; set; } = "";
        public int Interval { get; set; } = 10000;
    }
}
