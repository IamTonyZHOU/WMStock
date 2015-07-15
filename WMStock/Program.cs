using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace WMStock
{
    class Program
    {
        private static String ACCESS_TOKEN = "";//put your token here

        static void Main(string[] args)
        {
            RunAsync().Wait();
        }

        static async Task RunAsync()
        {
            if (string.IsNullOrEmpty(ACCESS_TOKEN))
                throw new ArgumentException("Please set your access token first!");

            using (var client = new HttpClient())
            {
                client.Timeout = new TimeSpan(0, 0, 60);
                client.BaseAddress = new Uri("https://api.wmcloud.com/");
                client.DefaultRequestHeaders.Add("User-Agent", "WinInetGet/0.1");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + ACCESS_TOKEN);

                // HTTP GET
                var response = await client.GetByteArrayAsync("data/v1/api/market/getMktEqud.json?field=&beginDate=&endDate=&secID=&ticker=600030&tradeDate=20150205");
                var responseString = Encoding.UTF8.GetString(response, 0, response.Length - 1);
                Console.WriteLine(responseString);
            }
        }
    }
}
