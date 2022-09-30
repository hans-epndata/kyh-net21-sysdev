using Microsoft.Azure.Devices.Client;

namespace Device.Console.TemperatureApp;

class Program
{
    private static readonly DeviceClient _deviceClient = DeviceClient.CreateFromConnectionString("HostName=kyh-shared-iothub.azure-devices.net;DeviceId=consoleapp;SharedAccessKey=Ra+ZfzVdHdmKGehCdSK4Y6PFYZGN28AcqOFTTSjOPpc=");
    private static bool lightState = false;


    public static void Main()
    {
        ConfigureDeviceAsync().ConfigureAwait(false);

        System.Console.ReadKey();
    }


    public static async Task ConfigureDeviceAsync()
    {
        await _deviceClient.SetMethodHandlerAsync("GetDeviceName", GetDeviceNameAsync, null);
        var twin = _deviceClient.GetTwinAsync();
    }

    public static Task<MethodResponse> GetDeviceNameAsync(MethodRequest methodRequest, object userContext)
    {
        lightState = !lightState;
        
        System.Console.WriteLine($"Method {methodRequest.Name} has been triggred. LightState is {lightState}");
        return Task.FromResult(new MethodResponse(new byte[0], 200));
    }
}