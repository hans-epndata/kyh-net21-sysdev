using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DeviceClient deviceClient;
        private readonly string deviceId = "dcfcb632-2ae0-4e76-98c5-1700592dc77a";
        private string connectionString = "";
        private bool isConnected = false;

        public MainWindow()
        {
            InitializeComponent();

            ConnectAsync().ConfigureAwait(false);
            Task.FromResult(SendMessageAsync());
            //Task.Run(async () => await SendMessageAsync());
        }

        private async void btnConnect_Click(object sender, RoutedEventArgs e)
        {
            await ConnectAsync();
        }


        private async Task ConnectAsync()
        {
            if (!isConnected)
            {
                tblockStatus.Text = "Initializing. Please wait...";

                using var http = new HttpClient();
                var response = await http.PostAsJsonAsync("https://kyh-fa.azurewebsites.net/api/devices", new { DeviceId = deviceId });

                if (response.IsSuccessStatusCode)
                {
                    isConnected = true;
                    var data = JsonConvert.DeserializeObject<dynamic>(await response.Content.ReadAsStringAsync());
                    connectionString = data?.deviceConnectionString;
                    deviceClient = DeviceClient.CreateFromConnectionString(connectionString);

                    if (isConnected)
                    {
                        btnConnect.IsEnabled = false;
                        btnConnect.Content = "Connected";
                    }                      
                    else
                    {
                        btnConnect.IsEnabled = true;
                        btnConnect.Content = "Connect";
                    }

                    tblockStatus.Text = "Initializing was successful";
                }
            }
        }

        public async Task SendMessageAsync()
        {
            while (true)
            {
                if (isConnected)
                {
                    var message = new Message(Encoding.UTF8.GetBytes("hejsan"));
                    await deviceClient.SendEventAsync(message);
                    tblockStatus.Text = "Message sent to Azure Iot Hub " + DateTime.Now.ToString(); 
                }
                await Task.Delay(5000);
            }
  
        }

    }
}
