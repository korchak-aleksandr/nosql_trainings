using System;
using System.Net.Http;
using NoSqlStress.WebApiClient;

namespace NoSqlStress.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var httpClient = new HttpClient();

            var apiClient = new HttpWebApiClient("http://localhost:5660", httpClient);

            var response = apiClient.WeatherForecastAsync().GetAwaiter().GetResult();

            foreach (var item in response)
            {
                Console.WriteLine($"{item.Date} - {item.Summary}");
            }

            Console.WriteLine("Hello World!");
        }
    }
}
