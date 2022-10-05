using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCF.Server.Models
{
    internal class Message
    {
        public DateTime Created { get; set; }
        public int Temperature { get; set; }
        public int Humidity { get; set; }
    }
}
