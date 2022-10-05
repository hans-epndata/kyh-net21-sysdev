using SmartApp.MVVM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartApp.Helpers
{
    internal class NavigateCommand<T> : BaseCommand where T : BaseViewModel
    {
        private readonly Func<T> _createViewModel;

        public NavigateCommand(Func<T> createViewModel)
        {
            _createViewModel = createViewModel;
        }

        public override void Execute(object? parameter)
        {
            
        }
    }
}
