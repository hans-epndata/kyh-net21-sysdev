using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Azure.Devices;
using AzureFunctions.Models;

namespace AzureFunctions
{
    public static class ConnectDevice
    {
        private static readonly RegistryManager registryManager =
            RegistryManager.CreateFromConnectionString(Environment.GetEnvironmentVariable("IotHub"));


        [FunctionName("ConnectDevice")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "devices/connect")] HttpRequest req,
            ILogger log)
        {

            try
            {
                var body = JsonConvert.DeserializeObject<HttpDeviceRequest>(await new StreamReader(req.Body).ReadToEndAsync());
                if (string.IsNullOrEmpty(body.DeviceId))
                    return new BadRequestObjectResult(new HttpDeviceResponse("DeviceId is required"));

                var device = await registryManager.GetDeviceAsync(body.DeviceId);
                if (device == null)
                    device = await registryManager.AddDeviceAsync(new Device(body.DeviceId));

                if (device != null)
                {
                    var twin = await registryManager.GetTwinAsync(device.Id);
                    twin.Properties.Desired["interval"] = 10000;
                    await registryManager.UpdateTwinAsync(twin.DeviceId, twin, twin.ETag);
                }

                return new OkObjectResult(new HttpDeviceResponse("Device connected", device));

            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(new HttpDeviceResponse("Unable to connect the device", ex.Message));
            }

        }
    }
}
