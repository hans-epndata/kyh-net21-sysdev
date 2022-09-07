using Microsoft.Azure.Devices.Shared;
using Microsoft.Azure.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureFunctions.Models
{
    internal class AddDeviceResponse
    {
        public string Message { get; set; } = "Device was successfully added";
        public string DeviceConnectionString => $"HostName={IotHubName};DeviceId={Device.Id};SharedAccessKey={Device.Authentication.SymmetricKey.PrimaryKey}";
        public string IotHubName { get; set; } = Environment.GetEnvironmentVariable("IotHubName");
        public Device Device { get; set; } = new();
        public Twin DeviceTwin { get; set; } = new();
    }
}
