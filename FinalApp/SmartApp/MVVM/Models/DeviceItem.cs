using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartApp.MVVM.Models
{
    internal class DeviceItem
    {
        public string DeviceId { get; set; } = "";
        public string DeviceName { get; set; } = "";
        public string DeviceType { get; set; } = "";
        public string Location { get; set; } = "";
        public bool DeviceState { get; set; }
        public string IconActive { get; set; } = "";
        public string IconInActive { get; set; } = "";
    }
}
