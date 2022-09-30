using Microsoft.Azure.Devices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Device.UWP.TemperatureApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        private readonly ServiceClient serviceClient = ServiceClient.CreateFromConnectionString("HostName=kyh-shared-iothub.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=/5asl5agNK3raYZNyfkumb0vcsnT+OdUeoUOupOWLQo=");
        private readonly RegistryManager registryManager = RegistryManager.CreateFromConnectionString("HostName=kyh-shared-iothub.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=/5asl5agNK3raYZNyfkumb0vcsnT+OdUeoUOupOWLQo=");

        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void btnDirectMethod_Click(object sender, RoutedEventArgs e)
        {
            tblock_Status.Text = "";

            var methodInvoke = new CloudToDeviceMethod("GetDeviceName");
            var result = await serviceClient.InvokeDeviceMethodAsync("consoleapp", methodInvoke);
            tblock_Status.Text = $"{result.Status}";
        }

        private async void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            var device = await registryManager.GetDeviceAsync(tb_DeviceId.Text);
            await registryManager.RemoveDeviceAsync(device);
        }
    }
}
