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
        private List<DeviceItem> _devices = new()
        {
            new DeviceItem { DeviceId = "device1", DeviceName = "Intelli-Light", DeviceType="Light" },
            new DeviceItem { DeviceId = "device2", DeviceName = "Intelli-Fan", DeviceType="Fan" },
            new DeviceItem { DeviceId = "device3", DeviceName = "Intelli-Ledstrip", DeviceType="Led Strip" }
        };

        public KitchenViewModel()
        {
            Devices = new();
            GetDevices();
        }

        public string Title { get; set; } = "Kitchen";
        public string Temperature { get; set; } = "23 °C";
        public string Humidity { get; set; } = "34 %";

        public List<DeviceItem> Devices { get; set; }


        private void GetDevices()
        {
            foreach(var device in _devices)
            {
                switch(device.DeviceType.ToLower())
                {
                    case "fan":
                        device.IconActive = "\uf863";
                        device.IconInActive = "\uf863";
                        device.StateActive = "ON";
                        device.StateInActive = "OFF";
                        break;

                    case "light":
                        device.IconActive = "\uf672";
                        device.IconInActive = "\uf0eb";
                        device.StateActive = "ON";
                        device.StateInActive = "OFF";
                        break;

                    default:
                        device.IconActive = "\uf2db";
                        device.IconInActive = "\uf2db";
                        device.StateActive = "ENABLE";
                        device.StateInActive = "DISABLE";
                        break;
                }

                Devices.Add(device);
            }
        }
    }
}
