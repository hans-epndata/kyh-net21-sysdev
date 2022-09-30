using Device.WPF.TemperatureApp.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Net.Http;
using System.Net.Http.Json;

namespace Device.WPF.TemperatureApp.Services
{
    public class ConfigurationService
    {
        private readonly string sql_connectionstring = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\HansMattin-Lassei\\Documents\\Utbildning\\NET21\\Lektion-12\\Device.WPF.TemperatureApp\\Data\\device_wpf_temperature.mdf;Integrated Security=True;Connect Timeout=30";

        public async Task<bool> IsConfiguredAsync()
        {
            var isConfigured = false;

            try
            {
                using var conn = new SqlConnection(sql_connectionstring);
                isConfigured = await conn.QueryFirstOrDefaultAsync<bool>("SELECT IsConfigured FROM DeviceInformation");
            }
            catch { }

            return isConfigured;
        }

        public async Task<DeviceInformation> ConnectDeviceAsync(string url, DeviceInformation deviceInformation)
        {
            using var http = new HttpClient();
            var result = await http.PostAsJsonAsync(url, new { deviceId = deviceInformation.DeviceId });
            deviceInformation.ConnectionString = await result.Content.ReadAsStringAsync();
            return deviceInformation;
        }

        public async Task SaveYourInformationAsync(YourInformation yourInformation)
        {
            using var conn = new SqlConnection(sql_connectionstring);
            await conn.ExecuteAsync("INSERT INTO YourInformation VALUES (@Email, @FirstName, @LastName)", yourInformation);
        }

        public async Task SaveDeviceInformationAsync(DeviceInformation deviceInformation)
        {
            using var conn = new SqlConnection(sql_connectionstring);
            await conn.ExecuteAsync("INSERT INTO DeviceInformation VALUES (@DeviceId, @DeviceType, @DeviceName, @Location, @ConnectionString, @IsConfigured)", deviceInformation);
        }
    }
}
