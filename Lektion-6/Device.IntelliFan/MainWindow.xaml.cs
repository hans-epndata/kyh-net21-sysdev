using Microsoft.Azure.Devices;
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
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Device.IntelliFan
{
    enum ConnectionState
    {
        NotConnected,
        Connecting,
        StillConnecting,
        Initializing,
        Connected
    }


    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ConnectAsync().ConfigureAwait(false);
        }

        private int interval = 10000;
        private bool isConnected;
        private string deviceId = "intellifan-e001";
        private DeviceClient deviceClient;

        private void SetConnectionState(ConnectionState state)
        {
            switch(state)
            {
                case ConnectionState.NotConnected:
                    tblockConnectionState.Text = "Not connected";
                    btnToggle.Visibility = Visibility.Visible;
                    btnToggle.Content = "Connect";
                    break;

                case ConnectionState.Connecting:
                    tblockConnectionState.Text = "Connecting. Please wait...";
                    btnToggle.Visibility = Visibility.Hidden;
                    break;

                case ConnectionState.StillConnecting:
                    tblockConnectionState.Text = "Still connecting. Please wait...";
                    btnToggle.Visibility = Visibility.Hidden;
                    break;

                case ConnectionState.Initializing:
                    tblockConnectionState.Text = "Initializing. Please wait...";
                    btnToggle.Visibility = Visibility.Hidden;
                    break;

                case ConnectionState.Connected:
                    isConnected = true;
                    tblockConnectionState.Text = "Connected";
                    btnToggle.Visibility = Visibility.Visible;
                    btnToggle.Content = "Start Fan";

                    break;

            }
        }

        private async Task ConnectAsync()
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
                            var response = await http.PostAsJsonAsync("https://kyh-shared.azurewebsites.net/api/devices/connect", new HttpDeviceRequest { DeviceId = deviceId });
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
                                        twinCollection["owner"] = "Hans";
                                        twinCollection["deviceType"] = "FAN";

                                        await deviceClient.UpdateReportedPropertiesAsync(twinCollection);
                                        var twin = deviceClient.GetTwinAsync();

                                        var result = await http.GetAsync($"https://kyh-shared.azurewebsites.net/api/devices/connect?code=-SWiKVko3aRE3PV53iizVYEXzE84M8hpmxI39vqvXxqfAzFus0uGDA==&deviceId={deviceId}");
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


        private void btnToggle_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
