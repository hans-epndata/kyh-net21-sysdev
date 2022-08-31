using Microsoft.Azure.Devices;

namespace WebApi.Services
{
    public interface IDeviceService
    {
        public Task<string> CreateDeviceAsync(string deviceId);
    }

    public class DeviceService : IDeviceService
    {
        private readonly RegistryManager _registryManager;
        private readonly IConfiguration _config;

        public DeviceService(IConfiguration config)
        {
            _config = config;
            _registryManager = RegistryManager.CreateFromConnectionString(_config.GetConnectionString("IotHub"));
        }

        public async Task<string> CreateDeviceAsync(string deviceId)
        {
            var device = await _registryManager.AddDeviceAsync(new Device(deviceId));
            return $"HostName=kyh-iothub-1.azure-devices.net;DeviceId={device.Id};SharedAccessKey={device.Authentication.SymmetricKey.PrimaryKey}";
        }

        public Task<Device> GetDeviceAsync(string deviceId)
        {
            throw new NotImplementedException();
        }
    }
}
