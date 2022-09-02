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

namespace AsyncAwait.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            tblockStatus.Text += Task.Run(() => DownloadingAsync()).Result;
        }

        private void btnBlockingCode_Click(object sender, RoutedEventArgs e)
        {
            tblockStatus.Text = "";

            tblockStatus.Text += "Downloading...\n";
            tblockStatus.Text += DownloadingAsync().Result;

            tblockStatus.Text += "Installing...\n";
            tblockStatus.Text += InstallingAsync().Result;

            tblockStatus.Text += "Blocking Code Done\n";
        }

        private async void btnNonBlockingCode_Click(object sender, RoutedEventArgs e)
        {
            tblockStatus.Text = "";

            tblockStatus.Text += "Downloading...\n";
            tblockStatus.Text += await DownloadingAsync();

            tblockStatus.Text += "Installing...\n";
            tblockStatus.Text += await InstallingAsync();

            tblockStatus.Text += "Non-Blocking Code Done\n";
        }


        private async Task<string> DownloadingAsync()
        {
            await Task.Delay(5000);
            return "Downloading completed\n";
        }

        private async Task<string> InstallingAsync()
        {       
            await Task.Delay(5000);
            return "Installing completed\n";
        }

    }
}
