using Microsoft.Azure.Devices.Client;
using Microsoft.Azure.Devices.Shared;
using Newtonsoft.Json;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfShared.Helpers
{
    public static class DeviceManager
    {
        public enum ConnectionState
        {
            NotConnected,
            Connecting,
            StillConnecting,
            Initializing,
            Connected
        }


        private static DeviceClient deviceClient;
        private static bool isConnected = false;
        private static string baseUrl = "https://kyh-shared.azurewebsites.net/api/devices/connect";

        public static string ConnectionStateMessage { get; private set; }
        public static string DeviceId { get; private set; }
        public static string Owner { get; private set; }
        public static string DeviceType { get; private set; }


        public static void Initialize(string deviceId, string deviceType, string owner)
        {
            DeviceId = deviceId;
            DeviceType = deviceType;
            Owner = owner;
        }

        public static void SetConnectionState(ConnectionState state)
        {
            switch (state)
            {
                case ConnectionState.NotConnected:
                    ConnectionStateMessage = "Not connected";
                    break;

                case ConnectionState.Connecting:
                    ConnectionStateMessage = "Connecting. Please wait...";
                    break;

                case ConnectionState.StillConnecting:
                    ConnectionStateMessage = "Still connecting. Please wait...";
                    break;

                case ConnectionState.Initializing:
                    ConnectionStateMessage = "Initializing. Please wait...";
                    break;

                case ConnectionState.Connected:
                    isConnected = true;
                    ConnectionStateMessage = "Connected";
                    break;

            }

        }

        public static async Task ConnectAsync()
        {
            for (int i = 0; i < 10; i++)
            {
                if (!isConnected)
                {
                    // If more then 5 attempts then change connectionState
                    SetConnectionState(i > 5 ? ConnectionState.StillConnecting : ConnectionState.Connecting);

                    try
                    {
                        using (var http = new HttpClient())
                        {
                            var response = await http.PostAsJsonAsync(baseUrl, new HttpDeviceRequest { DeviceId = DeviceId });
                            if (response.IsSuccessStatusCode)
                            {
                                var data = JsonConvert.DeserializeObject<HttpDeviceResponse>(await response.Content.ReadAsStringAsync());
                                if (data != null)
                                {
                                    try
                                    {
                                        deviceClient = DeviceClient.CreateFromConnectionString(data.ConnectionString);

                                        SetConnectionState(ConnectionState.Initializing);
                                        var twinCollection = new TwinCollection();
                                        twinCollection["owner"] = Owner;
                                        twinCollection["deviceType"] = DeviceType;

                                        await deviceClient.UpdateReportedPropertiesAsync(twinCollection);
                                        var twin = deviceClient.GetTwinAsync();

                                        var result = await http.GetAsync($"{baseUrl}?code=-SWiKVko3aRE3PV53iizVYEXzE84M8hpmxI39vqvXxqfAzFus0uGDA==&deviceId={DeviceId}");
                                        if (result.IsSuccessStatusCode)
                                        {
                                            var connectionstate = await result.Content.ReadAsStringAsync();
                                            if (connectionstate == "Connected")
                                            {
                                                SetConnectionState(ConnectionState.Connected);
                                                break;
                                            }
                                        }
                                    }
                                    catch { break; }
                                }
                            }

                            await Task.Delay(1000);
                        }

                    }
                    catch { }
                }
                else
                    SetConnectionState(ConnectionState.Connected);
            }

            if (!isConnected)
                SetConnectionState(ConnectionState.NotConnected);
        }
    }
}
