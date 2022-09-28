using Microsoft.Azure.Devices.Client;
using System;
using System.Collections.Generic;
using System.Linq;
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
using Microsoft.Data.SqlClient;
using Dapper;
using System.Data;
using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.VisualBasic;
using Newtonsoft.Json.Linq;
using Microsoft.Azure.Devices.Shared;
using Newtonsoft.Json;
using Device.WPF.Models;

namespace Device.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly string _connect_url = "http://localhost:7138/api/devices/connect";
        private readonly string _connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\HansMattin-Lassei\\Documents\\Utbildning\\NET21\\Lektion-11\\Device.WPF\\Data\\device_db.mdf;Integrated Security=True;Connect Timeout=30";
        private DeviceClient _deviceClient;
        private DeviceInfo deviceInfo;

        private string _deviceId = "";        
        private bool _lightState = false;
        private bool _lightPrevState = false;
        private bool _connected = false;
        private int _interval = 1000;



        public MainWindow()
        {
            InitializeComponent();
            SetupAsync().ConfigureAwait(false);
            SendMessageAsync().ConfigureAwait(false);
        }


        private async Task SetupAsync()
        {
            tbStateMessage.Text = "Initializing Device. Please wait...";

            using IDbConnection conn = new SqlConnection(_connectionString);
            _deviceId = await conn.QueryFirstOrDefaultAsync<string>("SELECT DeviceId FROM DeviceInfo");
            if (string.IsNullOrEmpty(_deviceId))
            {
                tbStateMessage.Text = "Generating new DeviceID";
                _deviceId = Guid.NewGuid().ToString();
                await conn.ExecuteAsync("INSERT INTO DeviceInfo (DeviceId,DeviceName,DeviceType,Location,Owner) VALUES (@DeviceId, @DeviceName, @DeviceType, @Location, @Owner)", new { DeviceId = _deviceId, DeviceName = "WPF Device", DeviceType = "light", Location = "kitchen", Owner = "Hans Mattin-Lassei" });
            }      

            var device_ConnectionString = await conn.QueryFirstOrDefaultAsync<string>("SELECT ConnectionString FROM DeviceInfo WHERE DeviceId = @DeviceId", new { DeviceId = _deviceId });
            if (string.IsNullOrEmpty(device_ConnectionString))
            {
                tbStateMessage.Text = "Initializing ConnectionString. Please wait...";
                using var http = new HttpClient();
                var result = await http.PostAsJsonAsync(_connect_url, new { deviceId = _deviceId });
                device_ConnectionString = await result.Content.ReadAsStringAsync();
                await conn.ExecuteAsync("UPDATE DeviceInfo SET ConnectionString = @ConnectionString WHERE DeviceId = @DeviceId", new { DeviceId = _deviceId, ConnectionString = device_ConnectionString });
            }

            _deviceClient = DeviceClient.CreateFromConnectionString(device_ConnectionString);

            tbStateMessage.Text = "Updating Twin Properties. Please wait...";

            deviceInfo = await conn.QueryFirstOrDefaultAsync<DeviceInfo>("SELECT * FROM DeviceInfo WHERE DeviceId = @DeviceId", new { DeviceId = _deviceId });
            
            var twinCollection = new TwinCollection();
            twinCollection["deviceName"] = deviceInfo.DeviceName;
            twinCollection["deviceType"] = deviceInfo.DeviceType;
            twinCollection["location"] = deviceInfo.Location;
            twinCollection["owner"] = deviceInfo.Owner;
            twinCollection["lightState"] = _lightState;

            await _deviceClient.UpdateReportedPropertiesAsync(twinCollection);

            _connected = true;
            tbStateMessage.Text = "Device Connected.";
        }

        private async Task SendMessageAsync()
        {
            while(true)
            {
                if(_connected)
                {
                    if (_lightState != _lightPrevState)
                    {
                        _lightPrevState = _lightState;
                        
                        // d2c
                        var json = JsonConvert.SerializeObject(new { lightState = _lightState });
                        var message = new Message(Encoding.UTF8.GetBytes(json));
                            message.Properties.Add("deviceName", deviceInfo.DeviceName);
                            message.Properties.Add("deviceType", deviceInfo.DeviceType);
                            message.Properties.Add("location", deviceInfo.Location);
                            message.Properties.Add("owner", deviceInfo.Owner);
                       
                        await _deviceClient.SendEventAsync(message);
                        tbStateMessage.Text = $"Message sent at {DateTime.Now}.";

                        // device twin (reported)
                        var twinCollection = new TwinCollection();
                        twinCollection["lightState"] = _lightState;
                        await _deviceClient.UpdateReportedPropertiesAsync(twinCollection);
                    }   
                }
                await Task.Delay(_interval);
            }
        }

        private void btnOnOff_Click(object sender, RoutedEventArgs e)
        {
            _lightState = !_lightState;

            if (_lightState)
                btnOnOff.Content = "Turn Off";
            else
                btnOnOff.Content = "Turn On";
        }
    }
}
