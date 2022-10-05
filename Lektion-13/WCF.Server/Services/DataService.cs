using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using WCF.Server.Contracts;
using WCF.Server.Models;
using Microsoft.Data.SqlClient;
using Dapper;
using Newtonsoft.Json;

namespace WCF.Server.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    internal class DataService : IDataServiceContract
    {
        private readonly string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\HansMattin-Lassei\\Documents\\Utbildning\\NET21\\Lektion-13\\WCF.Server\\sql_db.mdf;Integrated Security=True;Connect Timeout=30";

        public Message[] GetMessages()
        {
            var messages = new List<Message>();

            try
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    messages = conn.Query<Message>("SELECT * FROM Messages").ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return messages.ToArray();
        }

        public string GetMessagesAsJson()
        {
            using (var conn = new SqlConnection(connectionString))
            {
                return JsonConvert.SerializeObject(conn.Query<Message>("SELECT * FROM Messages"));
            }
        }

        public string[] GetStrings()
        {
            using(var conn = new SqlConnection(connectionString))
            {
                return conn.Query<string>("SELECT Name FROM Names").ToArray();
            }
        }

        public string PostMessage(Message message)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Execute("INSERT INTO Messages VALUES(@Created, @Temperature, @Humidity)", message);
                Console.WriteLine($"Mottaget: {JsonConvert.SerializeObject(message)}");
            }

            return "Tadaaa";
        }


    }
}
