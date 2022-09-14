using SmartApp.MVVM.Views;
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
            CurrentView = KitchenViewModel;

            KitchenViewCommand = new RelayCommand(x => { CurrentView = KitchenViewModel; });
            
        }
        
        
        
        private object _currentView;
        
        public RelayCommand KitchenViewCommand { get; set; }
        public KitchenViewModel KitchenViewModel { get; set; }


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
