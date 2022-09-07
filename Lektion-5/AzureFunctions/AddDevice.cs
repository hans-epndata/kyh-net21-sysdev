using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using AzureFunctions.Models;
using Microsoft.Azure.Devices;

namespace AzureFunctions
{
    public static class AddDevice
    {
        private static readonly RegistryManager registryManager = 
            RegistryManager.CreateFromConnectionString(Environment.GetEnvironmentVariable("IotHub"));

        [FunctionName("AddDevice")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "devices")] HttpRequest req,
            ILogger log)
        {
            try
            {
                var data = JsonConvert.DeserializeObject<AddDeviceRequest>(await new StreamReader(req.Body).ReadToEndAsync());
                if (data == null || data.DeviceId == null)
                    return new BadRequestObjectResult("deviceId is required");

                var device = await registryManager.GetDeviceAsync(data.DeviceId);
                if (device != null)
                {
                    return new ConflictObjectResult(new AddDeviceResponse
                    {
                        Message = "Device already exists",
                        Device = device,
                        DeviceTwin = await registryManager.GetTwinAsync(device.Id)
                    });
                }

                device = await registryManager.AddDeviceAsync(new Device(data.DeviceId));
                var twin = await registryManager.GetTwinAsync(device.Id);

                twin.Properties.Desired["sendInterval"] = 10000;
                await registryManager.UpdateTwinAsync(twin.DeviceId, twin, twin.ETag);

                return new OkObjectResult(new AddDeviceResponse
                {
                    Device = device,
                    DeviceTwin = await registryManager.GetTwinAsync(device.Id)
                });

            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(new
                {
                    Error = "Unable to add new device",
                    Exception = ex.Message
                });
            }        
        }
    }
}
