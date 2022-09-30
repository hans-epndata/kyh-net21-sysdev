using Device.WPF.TemperatureApp.Services;
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

namespace Device.WPF.TemperatureApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ConfigurationService _configurationService;

        public MainWindow()
        {
            InitializeComponent();
            _configurationService = new ConfigurationService();

            if (!Task.Run(() => _configurationService.IsConfiguredAsync()).Result)
            {
                this.Hide();
                SetupWizard.SetupWizard setupWizard = new SetupWizard.SetupWizard();
                setupWizard.Show();
            }
        }
    }
}
