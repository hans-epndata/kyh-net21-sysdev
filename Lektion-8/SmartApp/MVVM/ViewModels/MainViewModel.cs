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
            
            KitchenViewCommand = new RelayCommand(x => { CurrentView = KitchenViewModel;  });

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


    }
}
