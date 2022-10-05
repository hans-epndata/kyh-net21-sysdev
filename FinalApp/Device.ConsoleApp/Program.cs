using Microsoft.Data.SqlClient;
using Dapper;
using System.Net.Http.Json;
using Microsoft.Azure.Devices.Client;
using Microsoft.Azure.Devices.Shared;

DeviceClient deviceClient;
var interval = 10000;
var deviceName = "";
var deviceType = "";
var location = "";

var connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\HansMattin-Lassei\\Documents\\Utbildning\\NET21\\FinalApp\\Device.ConsoleApp\\device_consoleapp_db.mdf;Integrated Security=True;Connect Timeout=30";
using var conn = new SqlConnection(connectionString);

var deviceId = await conn.QueryFirstOrDefaultAsync<string>("SELECT DeviceId FROM Settings");
if (string.IsNullOrEmpty(deviceId))
{
    deviceId = Guid.NewGuid().ToString();
 
    using var client = new HttpClient();
    var result = await client.PostAsJsonAsync("http://localhost:7118/api/devices/connect", new { deviceId });
    var device_connectionString = await result.Content.ReadAsStringAsync();

    deviceClient = DeviceClient.CreateFromConnectionString(device_connectionString, TransportType.Mqtt);
    var twin = await deviceClient.GetTwinAsync();
    try { interval = (int)twin.Properties.Desired["interval"]; }
    catch { }

    var twinCollection = new TwinCollection();
    twinCollection["deviceName"] = deviceName;
    twinCollection["deviceType"] = deviceType;
    twinCollection["location"] = location;

    await deviceClient.UpdateReportedPropertiesAsync(twinCollection);
}
