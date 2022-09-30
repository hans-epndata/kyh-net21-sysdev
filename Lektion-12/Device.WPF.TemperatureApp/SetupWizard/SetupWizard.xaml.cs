using Device.WPF.TemperatureApp.Models;
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
using System.Windows.Shapes;

namespace Device.WPF.TemperatureApp.SetupWizard
{
    enum SetupWizardSteps
    {
        YourInformation,
        DeviceInformation,
        ConnectionInformation,
        Finished
    }


    public partial class SetupWizard : Window
    {
        private readonly ConfigurationService configurationService;
        private SetupWizardSteps currentStep;
        public YourInformation yourInformation;
        public DeviceInformation deviceInformation;
        public ConnectionInformation connectionInformation;

        public SetupWizard()
        {
            InitializeComponent();
            configurationService = new ConfigurationService();
            yourInformation = new YourInformation();
            deviceInformation = new DeviceInformation();
            connectionInformation = new ConnectionInformation();
            
            currentStep = SetupWizardSteps.YourInformation;
            CurrentPage.Content = new SetupWizard_YourInformation(ref yourInformation);
        }

        private void btn_Back_Click(object sender, RoutedEventArgs e)
        {
             switch(currentStep)
            {
                case SetupWizardSteps.ConnectionInformation:
                    currentStep = SetupWizardSteps.DeviceInformation;
                    CurrentPage.Content = new SetupWizard_DeviceInformation(ref deviceInformation);
                    tblock_Next.Text = "Next";

                    tblock_DeviceInformation.FontWeight = FontWeights.Bold;
                    tblock_Finish.FontWeight = FontWeights.Normal;

                    ellipse_DeviceInformation.Fill = new SolidColorBrush(Colors.LightBlue);
                    ellipse_Finish.Fill = new SolidColorBrush(Colors.LightGray);
                    break;

                case SetupWizardSteps.DeviceInformation:
                    currentStep = SetupWizardSteps.YourInformation;
                    CurrentPage.Content = new SetupWizard_YourInformation(ref yourInformation);
                    btn_Back.Visibility = Visibility.Hidden;

                    tblock_DeviceInformation.FontWeight = FontWeights.Normal;
                    tblock_YourInformation.FontWeight = FontWeights.Bold;

                    ellipse_DeviceInformation.Fill = new SolidColorBrush(Colors.LightGray);
                    ellipse_YourInformation.Fill = new SolidColorBrush(Colors.LightBlue);
                    break;

            }
        }

        private async void btn_Next_Click(object sender, RoutedEventArgs e)
        {
            switch(currentStep)
            {
                case SetupWizardSteps.YourInformation:
                    currentStep = SetupWizardSteps.DeviceInformation;
                    CurrentPage.Content = new SetupWizard_DeviceInformation(ref deviceInformation);
                    btn_Back.Visibility = Visibility.Visible;

                    tblock_DeviceInformation.FontWeight = FontWeights.Bold;
                    tblock_YourInformation.FontWeight = FontWeights.Normal;

                    ellipse_DeviceInformation.Fill = new SolidColorBrush(Colors.LightBlue);
                    ellipse_YourInformation.Fill = new SolidColorBrush(Colors.LightGray);

                    break;

                case SetupWizardSteps.DeviceInformation:
                    currentStep = SetupWizardSteps.ConnectionInformation;
                    CurrentPage.Content = new SetupWizard_Connection(ref connectionInformation);
                    tblock_Next.Text = "Finish";

                    tblock_DeviceInformation.FontWeight = FontWeights.Normal;
                    tblock_Finish.FontWeight = FontWeights.Bold;

                    ellipse_DeviceInformation.Fill = new SolidColorBrush(Colors.LightGray);
                    ellipse_Finish.Fill = new SolidColorBrush(Colors.LightBlue);

                    break;

                case SetupWizardSteps.ConnectionInformation:
                    currentStep = SetupWizardSteps.Finished;
                    break;

                case SetupWizardSteps.Finished:
                    await ConfigureDeviceAsync();
                    break;
            }
        }

        private async Task ConfigureDeviceAsync()
        {
            deviceInformation = await configurationService.ConnectDeviceAsync($"{connectionInformation.ConnectionEndpoint}?deviceId={deviceInformation.DeviceId}", deviceInformation);
            if (!string.IsNullOrEmpty(deviceInformation.ConnectionString))
            {
                deviceInformation.IsConfigured = true;
                await configurationService.SaveYourInformationAsync(yourInformation);
                await configurationService.SaveDeviceInformationAsync(deviceInformation);

                this.Hide();
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
            }
        }
    }
}
