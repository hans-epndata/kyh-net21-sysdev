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
    public static class GetDevice
    {
        private static readonly RegistryManager registryManager = 
            RegistryManager.CreateFromConnectionString(Environment.GetEnvironmentVariable("IotHub"));

        [FunctionName("GetDevice")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
           
            string deviceId = req.Query["deviceId"];
            if (!string.IsNullOrEmpty(deviceId))
            {
                var device = await registryManager.GetDeviceAsync(deviceId);
                if (device != null)
                    return new OkObjectResult($"HostName=kyh-iothub-1.azure-devices.net;DeviceId={device.Id};SharedAccessKey={device.Authentication.SymmetricKey.PrimaryKey}");

                return new NotFoundResult();
            }

            return new BadRequestResult();
            
        }
    }
}
