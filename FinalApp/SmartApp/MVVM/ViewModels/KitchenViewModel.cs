using Microsoft.Azure.Devices;
using Microsoft.Azure.Devices.Shared;
using SmartApp.Helpers;
using SmartApp.MVVM.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;

namespace SmartApp.MVVM.ViewModels
{
    internal class KitchenViewModel : BaseViewModel
    {
        private ObservableCollection<DeviceItem> _deviceItems;
        private RegistryManager _registryManager;

        public ObservableCollection<DeviceItem> DeviceItems
        {
            get => _deviceItems;
            set 
            { 
                _deviceItems = value;
                OnPropertyChanged();
            }
        }


        private readonly NavigationStore _navigationStore;
        public ICommand NavigateToSettings { get; }
        
        public string PageTitle => "Köket";

        private string? _currentTemperature;
        public string CurrentTemperature
        {
            get => _currentTemperature!;
            set
            {
                _currentTemperature = value;
                OnPropertyChanged();
            }
        }

        private string? _currentTime;
		public string CurrentTime
		{
			get => _currentTime!;
			set 
			{ 
				_currentTime = value; 
				OnPropertyChanged();
			}
		}

        private string? _currentDate;


        public string CurrentDate
        {
            get => _currentDate!;
            set
            {
                _currentDate = value;
                OnPropertyChanged();
            }
        }



        public KitchenViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
            DeviceItems = new ObservableCollection<DeviceItem>();
            _registryManager = RegistryManager.CreateFromConnectionString("HostName=kyh-shared-iothub.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=/5asl5agNK3raYZNyfkumb0vcsnT+OdUeoUOupOWLQo=");

            DispatcherTimer UpdateDateTime = new DispatcherTimer();
            UpdateDateTime.Interval = TimeSpan.FromSeconds(5);
            UpdateDateTime.Tick += timer_tick;
            UpdateDateTime.Start();
        }

        private async void timer_tick(object? sender, EventArgs e)
        {
            CurrentTime = DateTime.Now.ToString("HH:mm:ss");
            CurrentDate = DateTime.Now.ToString("dd MMMM yyyy");
            await PopulateDevicesAsync();
        }

        private async Task PopulateDevicesAsync()
        {
            var result = _registryManager.CreateQuery("select * from devices");
            if (result.HasMoreResults)
            {
                foreach (Twin twin in await result.GetNextAsTwinAsync())
                {
                    DeviceItems.Add(new DeviceItem
                    {
                        DeviceId = 
                    })
                }
            }
        }
    }
}
