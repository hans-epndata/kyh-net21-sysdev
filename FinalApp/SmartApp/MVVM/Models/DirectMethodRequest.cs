using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartApp.MVVM.Models
{
    internal class DirectMethodRequest
    {
        public string DeviceId { get; set; } = null!;
        public string MethodName { get; set; } = null!; 
        public object? Payload { get; set; }
    }
}
