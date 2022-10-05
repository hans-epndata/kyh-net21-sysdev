using Microsoft.Data.SqlClient;
using Dapper;
using System.Net.Http.Json;
using Microsoft.Azure.Devices.Client;
using Microsoft.Azure.Devices.Shared;
using Device.ConsoleApp.Models;

DeviceClient deviceClient;
DeviceSettings deviceSettings = new DeviceSettings();
var connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\HansMattin-Lassei\\Desktop\\device_consoleapp_db.mdf;Integrated Security=True;Connect Timeout=30";

using var conn = new SqlConnection(connectionString);
try
{
    var settings = await conn.QueryFirstOrDefaultAsync<DeviceSettings>("SELECT * FROM Settings");
    if (settings != null)
        deviceSettings = settings; 
}
catch { }


if (string.IsNullOrEmpty(deviceSettings.DeviceId))
{
    deviceSettings.DeviceId = Guid.NewGuid().ToString();
   
    Console.Write("Enter Device Name: ");
    deviceSettings.DeviceName = Console.ReadLine();
    Console.Write("Enter Device Type: ");
    deviceSettings.DeviceType = Console.ReadLine();
    Console.Write("Enter Location: ");
    deviceSettings.Location = Console.ReadLine();

    await conn.ExecuteAsync("INSERT INTO Settings VALUES (@DeviceId, @ConnectionString, @DeviceName, @DeviceType, @Location)", deviceSettings);
}

if (string.IsNullOrEmpty(deviceSettings.ConnectionString))
{
    using var client = new HttpClient();
    var result = await client.PostAsJsonAsync("http://localhost:7118/api/devices/connect", deviceSettings);
    deviceSettings.ConnectionString = await result.Content.ReadAsStringAsync();
    await conn.ExecuteAsync("UPDATE Settings SET ConnectionString = @ConnectionString WHERE DeviceId = @DeviceId", deviceSettings);
}

deviceClient = DeviceClient.CreateFromConnectionString(deviceSettings.ConnectionString, TransportType.Mqtt);
var twin = await deviceClient.GetTwinAsync();
try { deviceSettings.Interval = (int)twin.Properties.Desired["interval"]; } catch { }

var twinCollection = new TwinCollection();
twinCollection["deviceName"] = deviceSettings.DeviceName;
twinCollection["deviceType"] = deviceSettings.DeviceType;
twinCollection["location"] = deviceSettings.Location;
await deviceClient.UpdateReportedPropertiesAsync(twinCollection);

await deviceClient.SetMethodHandlerAsync("StartStop", StartStopAsync, null);

Task<MethodResponse> StartStopAsync(MethodRequest methodRequest, object userContext)
{
    Console.WriteLine($"Method {methodRequest.Name} has been triggred.");
    return Task.FromResult(new MethodResponse(new byte[0], 200));
}

Console.ReadKey();