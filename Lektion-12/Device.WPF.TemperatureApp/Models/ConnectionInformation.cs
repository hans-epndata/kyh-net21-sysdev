using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Device.WPF.TemperatureApp.Models
{
    public class ConnectionInformation
    {
        public string ConnectionEndpoint { get; set; } = "http://localhost:7229/api/devices/connect";
    }
}
