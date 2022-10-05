using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using WCF.Client.Contracts;
using WCF.Client.Models;

namespace WCF.Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("WCF-CLIENT (.NET FRAMEWORK)");
            Console.WriteLine("Tryck på en knapp för att fortsätta...");
            Console.ReadKey();

            var binding = new NetTcpBinding(SecurityMode.None);
            var channel = new ChannelFactory<IDataServiceContract>(binding);
            var proxy = channel.CreateChannel(new EndpointAddress("net.tcp://localhost:5555/api/data"));

            GetStrings(proxy);
            PostMessageAsJson(proxy);
            GetMessagesAsJson(proxy);
            GetMessages(proxy);

            Console.ReadKey();
        }

        private static void GetStrings(IDataServiceContract proxy)
        {
            var response = proxy?.GetStrings();
            if (response != null)
            {
                Console.WriteLine("\n Skriver ut namn");
                response.ToList().ForEach(x => Console.WriteLine(x));
                Console.ReadKey();
            }
        }

        private static void GetMessages(IDataServiceContract proxy)
        {
            var response = proxy?.GetMessages();
            if (response != null)
            {
                Console.WriteLine("\n Skriver ut meddelanden");
                foreach(var x in response)
                    Console.WriteLine($"{x.Created} - Temperatur: {x.Temperature}, Luftfuktighet: {x.Humidity}");

                Console.ReadKey();
            }
        }

        private static void GetMessagesAsJson(IDataServiceContract proxy)
        {
            var response = proxy?.GetMessagesAsJson();
            if (response != null)
            {
                Console.WriteLine("\n Skriver ut meddelanden som JSON");
                Console.WriteLine(response);

                Console.ReadKey();
            }
        }

        private static void PostMessageAsJson(IDataServiceContract proxy)
        {
            Console.WriteLine("\n Skickar information till WCF SERVERN");
            var text = proxy?.PostMessage(new Message { Created = DateTime.Now, Temperature = 55, Humidity = 66 });
            Console.WriteLine(text);
            Console.ReadKey();

        }

    }
}
