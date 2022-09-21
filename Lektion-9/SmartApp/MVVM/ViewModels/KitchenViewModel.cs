using Microsoft.Azure.Devices;
using SmartApp.MVVM.Cores;
using SmartApp.MVVM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartApp.MVVM.ViewModels
{
    internal class KitchenViewModel
    {
        public string Title { get; set; } = "Kitchen";
        public string Temperature { get; set; } = "23 °C";
        public string Humidity { get; set; } = "34 %";
        
    }
}
