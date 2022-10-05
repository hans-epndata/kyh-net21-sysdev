using Microsoft.Azure.Devices;
using Newtonsoft.Json;
using SmartApp.MVVM.Models;
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

namespace SmartApp.MVVM.Views
{
    /// <summary>
    /// Interaction logic for KitchenView.xaml
    /// </summary>
    public partial class KitchenView : UserControl
    {
        public KitchenView()
        {
            InitializeComponent();
        }

        private async void btn_DirectMethod_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var button = sender as Button;
                var deviceItem = (DeviceItem)button!.DataContext;
                using ServiceClient serviceClient = ServiceClient.CreateFromConnectionString("HostName=kyh-shared-iothub.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=/5asl5agNK3raYZNyfkumb0vcsnT+OdUeoUOupOWLQo=");

                var directMethod = new CloudToDeviceMethod("StartStop");
                //deviceMethod.SetPayloadJson(JsonConvert.SerializeObject(new { interval = 50000 }));
                var result = await serviceClient.InvokeDeviceMethodAsync(deviceItem.DeviceId, directMethod);
            }
            catch { }
            
        }
    }
}
