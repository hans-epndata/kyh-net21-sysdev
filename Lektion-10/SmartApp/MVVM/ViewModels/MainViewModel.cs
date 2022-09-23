using SmartApp.MVVM.Cores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartApp.MVVM.ViewModels
{
    internal class MainViewModel : ObservableObject
    {
        public MainViewModel()
        {
            KitchenViewModel = new();
            KitchenViewCommand = new RelayCommand(x => { CurrentView = KitchenViewModel; });

            BedroomViewModel = new();
            BedroomViewCommand = new RelayCommand(x => { CurrentView = BedroomViewModel; });

            LivingroomViewModel = new();
            LivingroomViewCommand = new RelayCommand(x => { CurrentView = LivingroomViewModel; });

            CurrentView = KitchenViewModel;
        }

        public RelayCommand KitchenViewCommand { get; set; }
        public KitchenViewModel KitchenViewModel { get; set; }
        public RelayCommand BedroomViewCommand { get; set; }
        public BedroomViewModel BedroomViewModel { get; set; }
        public RelayCommand LivingroomViewCommand { get; set; }
        public LivingroomViewModel LivingroomViewModel { get; set; }





        private object _currentView;
        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }


    }
}
