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
    public static class AddDevice
    {
        private static readonly RegistryManager registryManager =
            RegistryManager.CreateFromConnectionString(Environment.GetEnvironmentVariable("IotHub"));


        [FunctionName("AddDevice")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "devices")] HttpRequest req,
            ILogger log)
        {
            var data = JsonConvert.DeserializeObject<DeviceRequest>(await new StreamReader(req.Body).ReadToEndAsync());

            if (!string.IsNullOrEmpty(data.DeviceId))
            {
                var device = await registryManager.GetDeviceAsync(data.DeviceId);
                
                device ??= await registryManager.AddDeviceAsync(new Device(data.DeviceId));
                return new OkObjectResult(new DeviceResponse { Device = device });
            }

            return new BadRequestResult();
        }
    }
}
