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
            KitchenViewModel = new KitchenViewModel();
            BedroomViewModel = new BedroomViewModel();
            
            KitchenViewCommand = new RelayCommand(x => { CurrentView = KitchenViewModel;  });
            BedroomViewCommand = new RelayCommand(x => { CurrentView = BedroomViewModel; });

            CurrentView = KitchenViewModel;
        }


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

        public RelayCommand KitchenViewCommand { get; set; }
        public KitchenViewModel KitchenViewModel { get; set; }
        
        public RelayCommand BedroomViewCommand { get; set; }
        public BedroomViewModel BedroomViewModel { get; set; }

    }
}
