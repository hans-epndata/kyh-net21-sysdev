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

namespace AzureFunctions
{
    public static class GetDeviceConnectionState
    {
        private static readonly RegistryManager registryManager =
            RegistryManager.CreateFromConnectionString(Environment.GetEnvironmentVariable("IotHub"));

        [FunctionName("GetDeviceConnectionState")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "devices/connect")] HttpRequest req,
            ILogger log)
        {
            
            string deviceId = req.Query["deviceId"];
            var device = await registryManager.GetDeviceAsync(deviceId);
            if (device != null)
                return new OkObjectResult(device.ConnectionState.ToString());

            return new BadRequestResult();
        }
    }
}
