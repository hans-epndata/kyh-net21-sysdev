using System;
using System.Collections.Generic;
using System.Data;
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

        private bool isConnected;


        private void SetConnectionState(ConnectionState state)
        {

        }

        private async Task ConnectAsync()
        {
            for (int i = 0; i < 10; i++)
            {
                if (!isConnected)
                {
                    // If more then 3 attempts then change connectionState
                    SetConnectionState(i > 3 ? ConnectionState.StillConnecting : ConnectionState.Connecting);
                }
            }
        }


        private void btnToggle_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
