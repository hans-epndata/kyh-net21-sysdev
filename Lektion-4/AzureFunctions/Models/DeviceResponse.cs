using Microsoft.Azure.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureFunctions.Models
{
    internal class DeviceResponse
    {
        public string IotHub => "kyh-iot.azure-devices.net";
        public string DeviceConnectionString => $"HostName={IotHub};DeviceId={Device.Id};SharedAccessKey={Device.Authentication.SymmetricKey.PrimaryKey}";
        public Device Device { get; set; }
    }
}
