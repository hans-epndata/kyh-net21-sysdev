using Microsoft.Azure.Devices;
using Microsoft.Azure.Devices.Shared;

namespace WebApi.Repositories
{
    public interface IDeviceManager
    {
        public Task<string> CreateDeviceAsync(string deviceId);
        public Task<Twin> GetDeviceAsync(string deviceId);
        public Task<IEnumerable<Twin>> GetDevicesAsync(string query = "SELECT * FROM devices");
        public Task<bool> DeleteDeviceAsync(string deviceId);
    }

    public class DeviceRepository : IDeviceManager
    {        
        private readonly IConfiguration _config;
        private readonly RegistryManager _registryManager;

        public DeviceRepository(IConfiguration config)
        {
            _config = config;
            _registryManager = RegistryManager.CreateFromConnectionString(_config.GetValue<string>("IotHub"));
        }

        public async Task<string> CreateDeviceAsync(string deviceId)
        {
            try
            {
                var device = await _registryManager.AddDeviceAsync(new Device(deviceId));

                if (device != null)
                    return device.Authentication.SymmetricKey.PrimaryKey;
            }
            catch { }
            return null!;
        }

        public async Task<Twin> GetDeviceAsync(string deviceId)
        {
            try
            {
                var twin = await _registryManager.GetTwinAsync((await _registryManager.GetDeviceAsync(deviceId)).Id);

                if (twin != null)
                    return twin;
            }
            catch { }
            return null!;
        }

        public async Task<IEnumerable<Twin>> GetDevicesAsync(string query = "SELECT * FROM devices")
        {
            var devices = new List<Twin>();

            try
            {
                var result = _registryManager.CreateQuery(query);

                if (result.HasMoreResults)
                    foreach (var twin in await result.GetNextAsTwinAsync())
                        devices.Add(twin);
            }
            catch { }
            return devices;
        }

        public async Task<bool> DeleteDeviceAsync(string deviceId)
        {
            try
            {
                var device = await _registryManager.GetDeviceAsync(deviceId);
                await _registryManager.RemoveDeviceAsync(device);
                return true;
            }
            catch { }
            return false;
        }
    }
}
