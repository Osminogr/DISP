using System;
using System.Net.Http;
using System.Text;
using Xamarin.Forms;

using App1.Domain;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace App1
{
    public static class Server
    {
        public static string url = "http://46.101.167.149:8003/api/";

        public static async void Request(string content, string type, string url1)
        {
            HttpClient client = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage();

            request.RequestUri = new Uri(url + url1 + "/");

            switch (type)
            {
                case "post":
                    {
                        request.Method = HttpMethod.Post;
                    }
                    break;
                case "put":
                    {
                        request.Method = HttpMethod.Put;
                    }
                    break;
            }

            System.Console.WriteLine(content);

            request.Content = new StringContent(content, Encoding.UTF8, "application/json");
            var ans = await client.SendAsync(request);
        }
    }
}
