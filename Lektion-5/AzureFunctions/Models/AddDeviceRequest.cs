using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureFunctions.Models
{
    internal class AddDeviceRequest
    {
        [Required]
        public string DeviceId { get; set; }
        public string DeviceType { get; set; }
        public string Location { get; set; }
        public string Owner { get; set; }
    }
}
