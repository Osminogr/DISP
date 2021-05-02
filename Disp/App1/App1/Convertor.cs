using System;
using System.Net.Http;
using System.Text;
using Xamarin.Forms;

using App1.Domain;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

namespace App1
{
    public static class Server
    {
        public static string ROOT_URL = "http://46.101.167.149:8003/api/";
        public static string AUTH_OBJECT = "AUTH_OBJECT";

        public static async Task<HttpResponseMessage> Request(string content, string type, string url)
        {
            HttpClient client = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage();

            request.RequestUri = new Uri(ROOT_URL + url + "/");

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

            request.Content = new StringContent(content, Encoding.UTF8, "application/json");
            var ans = await client.SendAsync(request);

            return ans;
        }

        public static Task<HttpResponseMessage> CreateCodeReq(string number)
        {
            return Server.Request(@"{""CodeReq"":{""phone"":""" + number + @"""}}", "post", "gcode");
        }

        public static async Task<Adv> GetAdv(string number)
        {
            Adv adv = null;

            HttpClient client = new HttpClient();

            var answer = await client.GetAsync(ROOT_URL + "adv/" + number);
            var responseBody = await answer.Content.ReadAsStringAsync();

            if (responseBody != null && responseBody.Contains(nameof(Adv)))
            {
                JObject j = JObject.Parse(responseBody);
                adv = JsonConvert.DeserializeObject<Adv>(j[nameof(Adv)].ToString());
            }

            return adv;
        }

        public static async Task<Driver> GetDriver(string number)
        {
            Driver driver = null;

            HttpClient client = new HttpClient();

            var answer = await client.GetAsync(ROOT_URL + "drivers/" + number);
            var responseBody = await answer.Content.ReadAsStringAsync();
            if (responseBody != null && responseBody.Contains(nameof(Driver)))
            {
                JObject j = JObject.Parse(responseBody);
                driver = JsonConvert.DeserializeObject<Driver>(j[nameof(Driver)].ToString());
            }

            return driver;
        }

        public static async Task<Entity> GetEntity(string code, string number)
        {
            Entity entity = null;

            HttpClient client = new HttpClient();

            var answer = await client.GetAsync(ROOT_URL + "gcode/?code=" + code + "&phone=" + number);
            var responseBody = await answer.Content.ReadAsStringAsync();

            if (responseBody != null && responseBody.Contains(nameof(Entity)))
            {
                JObject jObject = JObject.Parse(responseBody);

                string type = (string)jObject["type"];

                if (type == nameof(Adv))
                {
                    entity = JsonConvert.DeserializeObject<Adv>(jObject[nameof(Entity)].ToString());
                }
                else
                {
                    entity = JsonConvert.DeserializeObject<Driver>((string)jObject[nameof(Entity)]);
                }
            }

            return entity;
        }

        public static async Task<HttpContent> AddVideoRequest(VideoRequest videoRequest)
        {
            var uri = new Uri(string.Format(ROOT_URL + "videorequests/{0}", videoRequest.adv.id));
            var content = new MultipartFormDataContent();

            if (videoRequest.photos != null && videoRequest.photos.Count > 0)
            {
                int number = 1;
                foreach (var photo in videoRequest.photos)
                {
                    byte[] data;
                    using (var br = new BinaryReader(photo.data))
                    {
                        data = br.ReadBytes((int)photo.data.Length);
                    }

                    ByteArrayContent bytes = new ByteArrayContent(data);
                    content.Add(bytes, "photo" + number++, photo.name);
                }
            }

            content.Add(new StringContent(videoRequest.text, Encoding.UTF8), "text");
            

            var httpClient = new HttpClient();
            var httpResponseMessage = await httpClient.PostAsync(uri, content);

            return httpResponseMessage.Content;
        }

        public static async Task<HttpContent> LoadVideoAsync(Video video)
        {
            var uri = new Uri(string.Format(ROOT_URL + "uvideo/{0}", video.adv.id));
            var content = new MultipartFormDataContent();

            content.Add(new StreamContent(video.data), "file", video.name);

            var httpClient = new HttpClient();
            var httpResponseMessage = await httpClient.PostAsync(uri, content);

            return httpResponseMessage.Content;
        }

        public static async Task<HttpContent> AddAdv(Adv adv)
        {
            var uri = new Uri(string.Format(ROOT_URL + "adv/"));

            string json = JsonConvert.SerializeObject(adv);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var httpClient = new HttpClient();
            var httpResponseMessage = await httpClient.PostAsync(uri, content);

            return httpResponseMessage.Content;
        }

        public static async Task<HttpContent> AddDriver(Driver driver)
        {
            var uri = new Uri(string.Format(ROOT_URL + "drivers/"));

            string json = JsonConvert.SerializeObject(driver);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var httpClient = new HttpClient();
            var httpResponseMessage = await httpClient.PostAsync(uri, content);

            return httpResponseMessage.Content;
        }
    }
}
