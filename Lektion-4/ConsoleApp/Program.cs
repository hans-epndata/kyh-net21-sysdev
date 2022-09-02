using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;


namespace ConsoleApp
{
    class Program
    {
        private static readonly string deviceId = "758678aa-6cd6-4e65-8fb1-ef20d9e9129b";
        private static string connectionString = "";
        private static bool isConnected = false;
        private static DeviceClient deviceClient;

        public static async Task Main()
        {
            await InitializeAsync();

            while(isConnected)
            {
                await SendMessageAsync("hej");
                await Task.Delay(10000);
            }
            
        }

        public static async Task InitializeAsync()
        {
            Console.WriteLine("Intiializing. Please wait...");
            while (!isConnected)
            {
                if (string.IsNullOrEmpty(connectionString))
                {
                    using var http = new HttpClient();
                    var response = await http.PostAsJsonAsync("https://kyh-fa.azurewebsites.net/api/devices", new { DeviceId = deviceId });

                    if (response.IsSuccessStatusCode)
                    {
                        isConnected = true;
                        var data = JsonConvert.DeserializeObject<dynamic>(await response.Content.ReadAsStringAsync());
                        connectionString = data?.deviceConnectionString;
                        deviceClient = DeviceClient.CreateFromConnectionString(connectionString);
                    }

                }

                await Task.Delay(5000);
            }
        }

        public static async Task SendMessageAsync(string payload)
        {
            var message = new Message(Encoding.UTF8.GetBytes(payload));
            await deviceClient.SendEventAsync(message);
            Console.WriteLine("Message sent to Azure Iot Hub " + DateTime.Now.ToString());
        }

    }
}




























