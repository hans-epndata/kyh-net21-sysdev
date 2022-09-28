using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Device.WPF.Models
{
    internal class DeviceInfo
    {
        public string DeviceId { get; set; }
        public string ConnectionString { get; set; }
        public string DeviceName { get; set; }
        public string DeviceType { get; set; }
        public string Location { get; set; }
        public string Owner { get; set; }
    }
}
