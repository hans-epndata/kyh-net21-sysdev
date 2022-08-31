using Microsoft.Azure.Devices.Client;
using System.Net.Http.Json;
using System.Text;

namespace Device_Console
{
    class Program
    {
        private static DeviceClient _deviceClient;
        private static string _deviceId = "13a6026b-8b84-4879-afc6-da80a1467a15";

        public static async Task Main()
        {
            await InitializeAsync(_deviceClient, _deviceId);         
        }

        private static async Task InitializeAsync(DeviceClient deviceClient, string deviceId)
        {
            using var client = new HttpClient();

            var result = await client.GetAsync($"https://kyh-functionapp.azurewebsites.net/api/GetDevice?code=rJYrO8ANqdgixtyZ1TDOh5EZ4DaaByukHs1cmJxpHi-BAzFu6fadDw==&deviceId={deviceId}");
            var conn = await result.Content.ReadAsStringAsync();

            if (string.IsNullOrEmpty(conn))
            {
                result = await client.PostAsJsonAsync("https://kyh-functionapp.azurewebsites.net/api/CreateDevice?code=1mEglJnM4mPv4A1-eM5qlzlDCxSWmkdlQocqxCtOTt7XAzFu0AnwxQ==", new { deviceId = deviceId });
                conn = await result.Content.ReadAsStringAsync();
                
            }

            deviceClient = DeviceClient.CreateFromConnectionString(conn);
            Console.WriteLine($"Device Connection: {conn}");
            Console.WriteLine("Device Connected");
        }
    }
}