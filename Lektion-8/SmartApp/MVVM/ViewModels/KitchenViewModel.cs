using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartApp.MVVM.ViewModels
{
    internal class KitchenViewModel
    {
        public string Title { get; set; } = "Kitchen and Dining";
        public string Temperature { get; set; } = "23 °C";
        public string Humidity { get; set; } = "33 %";
    }
}
