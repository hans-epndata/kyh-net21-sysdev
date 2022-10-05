using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using WCF.Server.Contracts;
using WCF.Server.Services;

namespace WCF.Server
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IDataServiceContract dataService = new DataService();
            var uriArray = new Uri[1];
            uriArray[0] = new Uri("net.tcp://localhost:5555/api/data");

            ServiceHost serviceHost = new ServiceHost(dataService, uriArray);
            serviceHost.AddServiceEndpoint(typeof(IDataServiceContract), new NetTcpBinding(SecurityMode.None), "");
            serviceHost.Opened += OnServiceHostOpened;
            serviceHost.Open();

            Console.ReadKey();
        }

        private static void OnServiceHostOpened(object sender, EventArgs e)
        {
            Console.WriteLine("WCF Servern har startat och är i körläge.");
        }
    }
}
