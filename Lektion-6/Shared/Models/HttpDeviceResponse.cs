using Microsoft.Azure.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models
{
    public class HttpDeviceResponse
    {
        public HttpDeviceResponse()
        {

        }

        public HttpDeviceResponse(string message)
        {
            Message = message;
        }

        public HttpDeviceResponse(string message, string exception)
        {
            Message = message;
            Exception = exception;
        }


        public HttpDeviceResponse(string message, Device device)
        {
            Message = message;

            if (device != null)
                ConnectionString = $"HostName={Environment.GetEnvironmentVariable("IotHub")?.Split(";")[0].Split("=")[1]};DeviceId={device.Id};SharedAccessKey={device.Authentication.SymmetricKey.PrimaryKey}";
        }

        public string Message { get; set; }
        public string Exception { get; set; }
        public string ConnectionString { get; set; }
    }
}
