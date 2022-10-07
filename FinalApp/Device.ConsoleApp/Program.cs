using Microsoft.Data.SqlClient;
using Dapper;
using System.Net.Http.Json;
using Microsoft.Azure.Devices.Client;
using Microsoft.Azure.Devices.Shared;
using Device.ConsoleApp.Models;
using Newtonsoft.Json;
using System.Linq.Expressions;
using System.Text;

namespace Device.ConsoleApp;
class Program
{
    private static Twin twin;
    private static DeviceClient deviceClient;
    private static DeviceSettings settings = new DeviceSettings();
    private static string apiUri = "http://localhost:7118/api/devices/connect";
    private static string filePath = @$"{AppDomain.CurrentDomain.BaseDirectory}\configuration.json";

    public static async Task Main()
    {
        await GetConfigurationAsync();

        if (string.IsNullOrEmpty(settings.DeviceId))
            SetSettings();

        await SetConnectionStringAsync();
        await InitializeDeviceAsync();
        await SetIntervalAsync();
        await SetDeviceTwinAsync();
        await SetDirectMethodAsync();
        await SaveConfigurationAsync();

        Console.Clear();
        Console.WriteLine($"Device {settings.DeviceId} connected/configured and awaiting new commands.");
        Console.ReadKey();
    }

    private static async Task GetConfigurationAsync()
    {    
        try 
        {
            using var sr = new StreamReader(filePath);
            settings = JsonConvert.DeserializeObject<DeviceSettings>(await sr.ReadToEndAsync()); 
        }
        catch { }
    }

    private static async Task SaveConfigurationAsync()
    {    
        try 
        {
            using var sw = new StreamWriter(filePath);
            await sw.WriteLineAsync(JsonConvert.SerializeObject(settings)); 
        }
        catch { }
    }

    private static void SetSettings()
    {
        Console.Clear();
        Console.WriteLine("##### Device Settings Configuration #####\n");

        settings.DeviceId = Guid.NewGuid().ToString();
        Console.Write($"DeviceId: {settings.DeviceId}\n");
        
        Console.Write("Enter Device Name: ");
        settings.DeviceName = Console.ReadLine() ?? "";
        
        Console.Write("Enter Device Type: ");
        settings.DeviceType = Console.ReadLine() ?? "";
        
        Console.Write("Enter Location: ");
        settings.Location = Console.ReadLine() ?? "";

        Console.WriteLine("\n");
    }

    private static async Task SetConnectionStringAsync()
    {
        Console.Write("\nConfiguring connectionstring. Please wait...");

        using var client = new HttpClient();
        while (string.IsNullOrEmpty(settings.ConnectionString))
        {
            Console.Write(".");

            try
            {
                var result = await client.PostAsJsonAsync(apiUri, settings);
                if (result.IsSuccessStatusCode)
                    settings.ConnectionString = await result.Content.ReadAsStringAsync();
            }
            catch 
            {
                await Task.Delay(500);
            }
        }
    }

    private static async Task InitializeDeviceAsync()
    {
        Console.Write($"\nInitializing device {settings.DeviceId}. Please wait...");
        
        bool isConfigured = false;

        while(!isConfigured)
        {
            Console.Write(".");

            try 
            { 
                deviceClient = DeviceClient.CreateFromConnectionString(settings.ConnectionString, TransportType.Mqtt);
                twin = await deviceClient.GetTwinAsync();
            }
            catch { }

            if (deviceClient != null && twin != null)
                isConfigured = true;
            
            await Task.Delay(500);
        }
    }

    private static async Task SetIntervalAsync()
    {
        Console.Write($"\nConfiguring sending interval. Please wait...");

        try { settings.Interval = (int)twin.Properties.Desired["interval"]; } 
        catch 
        {
            Console.Write(" - Failed! No interval property found.");
        }
        await Task.Delay(500);
    }

    private static async Task SetDeviceTwinAsync()
    {
        Console.WriteLine("Configuring DeviceTwin Properties. Please wait...");

        var twinCollection = new TwinCollection();
        twinCollection["deviceName"] = settings.DeviceName;
        twinCollection["deviceType"] = settings.DeviceType;
        twinCollection["deviceState"] = settings.DeviceState;
        twinCollection["location"] = settings.Location;
        await deviceClient.UpdateReportedPropertiesAsync(twinCollection);
    }

    private static async Task SetDirectMethodAsync()
    {
        Console.WriteLine("Configuring Direct Method (ON/OFF). Please wait...");
        await deviceClient.SetMethodHandlerAsync("OnOff", OnOff, null);
    }

    private static Task<MethodResponse> OnOff(MethodRequest methodRequest, object userContext)
    {
        
            
        try
        {
            var data = JsonConvert.DeserializeObject<dynamic>(methodRequest.DataAsJson);
            Console.WriteLine($"Changing DeviceState from {settings.DeviceState} to {data!.deviceState}.");
            settings.DeviceState = data!.deviceState;

            SetDeviceTwinAsync().ConfigureAwait(false);
            SaveConfigurationAsync().ConfigureAwait(false);
            Console.WriteLine($"Device {settings.DeviceId} configured and awaiting new commands.");
            return Task.FromResult(new MethodResponse(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(settings)), 200));
        }
        catch (Exception ex)
        {
            return Task.FromResult(new MethodResponse(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(ex)), 400));
        }
        
    }

    private static async Task GetWeatherData()
    {
         var temperature = 0.0;
        var humidity = 0.0;
    }


    private static async Task SendDataAsync()
    {

        var temperature = 0.0;
        var humidity = 0.0;
        var json = JsonConvert.SerializeObject(new { temperature, humidity });
        var message = new Message(Encoding.UTF8.GetBytes(json));
    }
}











