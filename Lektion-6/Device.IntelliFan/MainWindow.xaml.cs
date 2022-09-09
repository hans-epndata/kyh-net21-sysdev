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
using WpfShared.Helpers;

namespace Device.IntelliFan
{


    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            UpdateConnectionState().ConfigureAwait(false);

            DeviceManager.Initialize("intellifan-e001", "FAN", "Hans");
            DeviceManager.ConnectAsync().ConfigureAwait(false);
        }


        private async Task UpdateConnectionState()
        {
            while(true)
            {
                tblockConnectionState.Text = DeviceManager.ConnectionStateMessage;
                if (DeviceManager.ConnectionStateMessage == "Connected")
                {
                    btnToggle.Visibility = Visibility.Visible;
                    btnToggle.Content = "Start Fan";
                }
                    
                if (DeviceManager.ConnectionStateMessage == "Not connected")
                {
                    btnToggle.Visibility = Visibility.Visible;
                    btnToggle.Content = "Connect";
                }

                await Task.Delay(1000);
            }
        }


        private async void btnToggle_Click(object sender, RoutedEventArgs e)
        {
            if (DeviceManager.ConnectionStateMessage == "Not connected")
                await DeviceManager.ConnectAsync();
        }
    }
}
