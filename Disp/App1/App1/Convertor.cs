using System;
using System.Net.Http;
using System.Text;
using Xamarin.Forms;

using App1.Domain;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Collections.Generic;
using Xamarin.Essentials;
using Newtonsoft.Json.Serialization;
using System.Linq;
using System.Reflection;
using App1.Domain.Json;
using System.Text.RegularExpressions;

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

        public static async Task<Adv> GetAdv(int id)
        {
            Adv adv = null;

            HttpClient client = new HttpClient();

            var answer = await client.GetAsync(ROOT_URL + "adv/" + id);
            var responseBody = await answer.Content.ReadAsStringAsync();

            if (responseBody != null && responseBody.Contains(nameof(Adv)))
            {
                JObject j = JObject.Parse(responseBody);
                adv = JsonConvert.DeserializeObject<Adv>(j[nameof(Adv)].ToString());
            }

            return adv;
        }

        public static async Task<List<Video>> GetVideos(Entity entity, bool isDriver)
        {
            List<Video> list = null;

            HttpClient client = new HttpClient();

            string url = "avideo/";
            if (isDriver) url = "dvideo/";

            var answer = await client.GetAsync(ROOT_URL + url + entity.id);
            var responseBody = await answer.Content.ReadAsStringAsync();

            if (responseBody != null) list = JsonConvert.DeserializeObject<List<Video>>(responseBody);
                
            return list;
        }

        public static async Task<Driver> GetDriver(int id)
        {
            Driver driver = null;

            HttpClient client = new HttpClient();

            var answer = await client.GetAsync(ROOT_URL + "driver/" + id);
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

                if (type == nameof(Adv)) entity = JsonConvert.DeserializeObject<Adv>(jObject[nameof(Entity)].ToString());
                else entity = JsonConvert.DeserializeObject<Driver>((string)jObject[nameof(Entity)].ToString());
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

            string json = JsonConvert.SerializeObject(adv, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            json = @"{""Adv"": " + json + "}";

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var httpClient = new HttpClient();
            var httpResponseMessage = await httpClient.PostAsync(uri, content);

            return httpResponseMessage.Content;
        }

        public static async Task<HttpContent> AddDriver(Driver driver)
        {
            var uri = new Uri(string.Format(ROOT_URL + "driver/"));
            var content = new MultipartFormDataContent();

            string json = JsonConvert.SerializeObject(driver, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            json = @"{""Driver"": " + json + "}";

            if (driver.driverLicence.photo1 != null) content.Add(new StreamContent(driver.driverLicence.photo1.data), "driverLicencePhoto1", driver.driverLicence.photo1.name);
            if (driver.driverLicence.photo2 != null) content.Add(new StreamContent(driver.driverLicence.photo2.data), "driverLicencePhoto2", driver.driverLicence.photo2.name);
            if (driver.driverLicence.photo3 != null) content.Add(new StreamContent(driver.driverLicence.photo3.data), "driverLicencePhoto3", driver.driverLicence.photo3.name);

            if (driver.car.photo1 != null) content.Add(new StreamContent(driver.car.photo1.data), "carPhoto1", driver.car.photo1.name);
            if (driver.car.photo2 != null) content.Add(new StreamContent(driver.car.photo2.data), "carPhoto2", driver.car.photo2.name);
            if (driver.car.photo3 != null) content.Add(new StreamContent(driver.car.photo3.data), "carPhoto3", driver.car.photo3.name);

            if (driver.person.passport.photo1 != null)content.Add(new StreamContent(driver.person.passport.photo1.data), "personPassportPhoto1", driver.person.passport.photo1.name);
            if (driver.person.passport.photo2 != null) content.Add(new StreamContent(driver.person.passport.photo2.data), "personPassportPhoto2", driver.person.passport.photo2.name);
            if (driver.person.passport.photo3 != null) content.Add(new StreamContent(driver.person.passport.photo3.data), "personPassportPhoto3", driver.person.passport.photo3.name);

            if (driver.person.selfPhoto != null) content.Add(new StreamContent(driver.person.selfPhoto.data), "personSelfPhoto", driver.person.selfPhoto.name);

            content.Add(new StringContent(json, Encoding.UTF8, "application/json"), "json");

            var httpClient = new HttpClient();
            var httpResponseMessage = await httpClient.PostAsync(uri, content);

            return httpResponseMessage.Content;
        }

        public static async Task<HttpContent> AddCompaign(Compaign compaign)
        {
            var uri = new Uri(ROOT_URL + "adreqcount/");

            JCompaign jCompaign = new JCompaign();
            jCompaign.adv = compaign.adv.id;
            jCompaign.tarif = compaign.tarif.id;
            jCompaign.video = compaign.video.id;

            string json = JsonConvert.SerializeObject(jCompaign, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            json = @"{""Compaign"": " + json + "}";

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var httpClient = new HttpClient();
            var httpResponseMessage = await httpClient.PostAsync(uri, content);

            return httpResponseMessage.Content;
        }

        public static async Task<HttpContent> SendMessage(Message message)
        {
            var uri = new Uri(string.Format(ROOT_URL + "message/"));

            string json = JsonConvert.SerializeObject(message, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            json = @"{""Message"": " + json + "}";

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var httpClient = new HttpClient();
            var httpResponseMessage = await httpClient.PostAsync(uri, content);

            return httpResponseMessage.Content;
        }

        public static async Task<List<Message>> GetMessages(Entity entity)
        {
            List<Message> messages = new List<Message>();

            HttpClient client = new HttpClient();

            var answer = await client.GetAsync(ROOT_URL + "message/" + entity.id);
            var responseBody = await answer.Content.ReadAsStringAsync();

            if (responseBody != null) messages = JsonConvert.DeserializeObject<List<Message>>(responseBody);

            return messages;
        }

        public static void SaveAuthObject(Entity entity, bool isCompany)
        {
            AuthObject authObject = new AuthObject();

            if (isCompany) authObject.adv = (Adv)entity;
            else authObject.driver = (Driver)entity;

            authObject.isCompany = isCompany;

            ClearAuthObject();
            Preferences.Set(Server.AUTH_OBJECT, JsonConvert.SerializeObject(authObject));
        }

        public static void ClearAuthObject()
        {
            Preferences.Clear();
        }

        public static void SaveShowAllDrivers(bool value)
        {
            string json = Preferences.Get(AUTH_OBJECT, null);

            if (json != null)
            {
                AuthObject authObject = JsonConvert.DeserializeObject<AuthObject>(json);

                if (authObject != null)
                {
                    authObject.showAllDrivers = value;
                    Preferences.Clear();
                    Preferences.Set(AUTH_OBJECT, JsonConvert.SerializeObject(authObject));
                }
            }
        }

        public static void SaveShowDriverAdReq(bool value)
        {
            string json = Preferences.Get(AUTH_OBJECT, null);

            if (json != null)
            {
                AuthObject authObject = JsonConvert.DeserializeObject<AuthObject>(json);

                if (authObject != null)
                {
                    authObject.showDriverAdReq = value;
                    Preferences.Set(AUTH_OBJECT, JsonConvert.SerializeObject(authObject));
                }
            }
        }

        public static AuthObject GetAuthObject()
        {
            string json = Preferences.Get(AUTH_OBJECT, null);

            if (json != null)
            {
                AuthObject authObject = JsonConvert.DeserializeObject<AuthObject>(json);

                if (authObject != null) return authObject;
            }

            return null;
        }

        public static async Task<List<Compaign>> GetCompaigns(int id)
        {
            List<Compaign> list = null;

            HttpClient client = new HttpClient();

            var answer = await client.GetAsync(ROOT_URL + "adreqcount/" + id);
            var responseBody = await answer.Content.ReadAsStringAsync();

            if (responseBody != null) list = JsonConvert.DeserializeObject<List<Compaign>>(responseBody);

            return list;
        }

        public static async Task<List<Tarif>> GetTarifs()
        {
            List<Tarif> tarifs = null;

            HttpClient client = new HttpClient();

            var answer = await client.GetAsync(ROOT_URL + "tarif/");
            var responseBody = await answer.Content.ReadAsStringAsync();

            if (responseBody != null) tarifs = JsonConvert.DeserializeObject<List<Tarif>>(responseBody);

            return tarifs;
        }

        public static async Task<List<Alert>> GetAlerts(Entity entity)
        {
            List<Alert> alerts = null;

            HttpClient client = new HttpClient();

            var answer = await client.GetAsync(ROOT_URL + "alert/" + entity.id);
            var responseBody = await answer.Content.ReadAsStringAsync();

            if (responseBody != null) alerts = JsonConvert.DeserializeObject<List<Alert>>(responseBody);

            return alerts;
        }

        public static async Task<HttpContent> SaveAccountNumber(AccountNumber accountNumber)
        {
            var uri = new Uri(string.Format(ROOT_URL + "an/{0}", accountNumber.id));

            string json = JsonConvert.SerializeObject(accountNumber, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            json = @"{""AccountNumber"": " + json + "}";

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var httpClient = new HttpClient();
            var httpResponseMessage = await httpClient.PutAsync(uri, content);

            return httpResponseMessage.Content;
        }

        public static async Task<HttpContent> SaveCompany(Company company)
        {
            var uri = new Uri(string.Format(ROOT_URL + "company/{0}", company.id));

            string json = JsonConvert.SerializeObject(company, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            json = @"{""Company"": " + json + "}";

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var httpClient = new HttpClient();
            var httpResponseMessage = await httpClient.PutAsync(uri, content);

            return httpResponseMessage.Content;
        }

        public static async Task<HttpContent> SavePerson(Person person)
        {
            var uri = new Uri(string.Format(ROOT_URL + "person/{0}", person.id));

            var content = new MultipartFormDataContent();

            string json = JsonConvert.SerializeObject(person, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            json = @"{""Person"": " + json + "}";

            content.Add(new StringContent(json, Encoding.UTF8, "application/json"), "Person");

            if (person.selfPhoto != null) content.Add(new StreamContent(person.selfPhoto.data), "photo1", person.selfPhoto.name);

            var httpClient = new HttpClient();
            var httpResponseMessage = await httpClient.PutAsync(uri, content);

            return httpResponseMessage.Content;
        }

        public static async Task<HttpContent> SavePassport(Passport passport)
        {
            var uri = new Uri(string.Format(ROOT_URL + "passport/{0}", passport.id));

            var content = new MultipartFormDataContent();

            string json = JsonConvert.SerializeObject(passport, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            json = @"{""Passport"": " + json + "}";

            content.Add(new StringContent(json, Encoding.UTF8, "application/json"), "Passport");

            if (passport.photo1 != null) content.Add(new StreamContent(passport.photo1.data), "photo1", passport.photo1.name);
            if (passport.photo2 != null) content.Add(new StreamContent(passport.photo2.data), "photo2", passport.photo2.name);
            if (passport.photo3 != null) content.Add(new StreamContent(passport.photo3.data), "photo3", passport.photo3.name);

            var httpClient = new HttpClient();
            var httpResponseMessage = await httpClient.PutAsync(uri, content);

            return httpResponseMessage.Content;
        }

        public static async Task<HttpContent> SaveCar(Car car)
        {
            var uri = new Uri(string.Format(ROOT_URL + "car/{0}", car.id));

            var content = new MultipartFormDataContent();

            string json = JsonConvert.SerializeObject(car, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            json = @"{""Car"": " + json + "}";

            content.Add(new StringContent(json, Encoding.UTF8, "application/json"), "Car");

            if (car.photo1 != null) content.Add(new StreamContent(car.photo1.data), "photo1", car.photo1.name);
            if (car.photo2 != null) content.Add(new StreamContent(car.photo2.data), "photo2", car.photo2.name);
            if (car.photo3 != null) content.Add(new StreamContent(car.photo3.data), "photo3", car.photo3.name);

            var httpClient = new HttpClient();
            var httpResponseMessage = await httpClient.PutAsync(uri, content);

            return httpResponseMessage.Content;
        }

        public static async Task<HttpContent> SaveDriverLicence(DriverLicence driverLicence)
        {
            var uri = new Uri(string.Format(ROOT_URL + "driverlicence/{0}", driverLicence.id));

            var content = new MultipartFormDataContent();

            string json = JsonConvert.SerializeObject(driverLicence, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            json = @"{""DriverLicence"": " + json + "}";

            content.Add(new StringContent(json, Encoding.UTF8, "application/json"), "DriverLicence");

            if (driverLicence.photo1 != null) content.Add(new StreamContent(driverLicence.photo1.data), "photo1", driverLicence.photo1.name);
            if (driverLicence.photo2 != null) content.Add(new StreamContent(driverLicence.photo2.data), "photo2", driverLicence.photo2.name);
            if (driverLicence.photo3 != null) content.Add(new StreamContent(driverLicence.photo3.data), "photo3", driverLicence.photo3.name);

            var httpClient = new HttpClient();
            var httpResponseMessage = await httpClient.PutAsync(uri, content);

            return httpResponseMessage.Content;
        }

        public static string GetPhoneFromRegex(string uiPhone)
        {
            string phone = null;

            string pattern = @"\(?([0-9]{3})\)?([ .-]?)([0-9]{3})(-?)([0-9]{2})(-?)([0-9]{2})";

            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);

            Match match = regex.Match(uiPhone);
            if (match.Groups != null && match.Groups.Count == 8) phone = match.Groups[1].Value + match.Groups[3].Value + match.Groups[5].Value + match.Groups[7].Value;

            return phone;
        }
    }
}
