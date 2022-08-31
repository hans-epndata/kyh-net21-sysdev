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
    public static class CreateDevice
    {
        private static readonly RegistryManager registryManager = 
            RegistryManager.CreateFromConnectionString(Environment.GetEnvironmentVariable("IotHub"));

        [FunctionName("CreateDevice")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonConvert.DeserializeObject<DeviceRequest>(requestBody);
            
            if (!string.IsNullOrEmpty(data.DeviceId)) 
            {
                try
                {
                    var device = await registryManager.AddDeviceAsync(new Device(data.DeviceId));
                    if (device != null)
                        return new OkObjectResult($"HostName=kyh-iothub-1.azure-devices.net;DeviceId={device.Id};SharedAccessKey={device.Authentication.SymmetricKey.PrimaryKey}");
                }
                catch { }
            }

            return new BadRequestResult();
        }
    }
}
