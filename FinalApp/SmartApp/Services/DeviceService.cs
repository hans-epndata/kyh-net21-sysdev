using Microsoft.Azure.Devices;
using Newtonsoft.Json;
using SmartApp.MVVM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SmartApp.Services
{
    internal interface IDeviceService
    {
        public Task<List<DeviceItem>> GetDevicesAsync(string query);
        public Task<CloudToDeviceMethodResult> SendDirectMethodAsync(DirectMethodRequest directMethodRequest);
    }

    internal class DeviceService : IDeviceService
    {
        private readonly string connectionString = "HostName=kyh-shared-iothub.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=/5asl5agNK3raYZNyfkumb0vcsnT+OdUeoUOupOWLQo=";

        public async Task<List<DeviceItem>> GetDevicesAsync(string query)
        {
            var devices = new List<DeviceItem>();

            try
            {
                using var registryManager = RegistryManager.CreateFromConnectionString(connectionString);
                var result = registryManager.CreateQuery(query);

                if (result.HasMoreResults)
                {
                    foreach (var twin in await result.GetNextAsTwinAsync())
                    {
                        var device = new DeviceItem
                        {
                            DeviceId = twin.DeviceId
                        };

                        try { device.DeviceName = twin.Properties.Reported["deviceName"].ToString(); }
                        catch { }
                        try { device.DeviceType = twin.Properties.Reported["deviceType"].ToString(); }
                        catch { }
                        try { device.Location = twin.Properties.Reported["location"].ToString(); }
                        catch { }
                        try { device.DeviceState = twin.Properties.Reported["deviceState"]; }
                        catch { }

                        switch (device.DeviceType.ToLower())
                        {
                            case "fan":
                                device.IconActive = "\uf863";
                                device.IconInActive = "\uf863";
                                break;

                            case "light":
                                device.IconActive = "\uf672";
                                device.IconInActive = "\uf0eb";
                                break;

                            default:
                                device.IconActive = "\uf2db";
                                device.IconInActive = "\uf2db";
                                break;
                        }

                        devices.Add(device);
                    }
                }
            }
            catch { }

            return devices;
        }

        public async Task<CloudToDeviceMethodResult> SendDirectMethodAsync(DirectMethodRequest directMethodRequest)
        {
            try
            {
                using var serviceClient = ServiceClient.CreateFromConnectionString(connectionString);

                var cloudToDeviceMethod = new CloudToDeviceMethod(directMethodRequest.MethodName);
                if (directMethodRequest.Payload != null)
                    cloudToDeviceMethod.SetPayloadJson(JsonConvert.SerializeObject(directMethodRequest.Payload));
                
                var result = await serviceClient.InvokeDeviceMethodAsync(directMethodRequest.DeviceId, cloudToDeviceMethod);
                return result;
            }
            catch { }

            return null!;
        }
    }
}
