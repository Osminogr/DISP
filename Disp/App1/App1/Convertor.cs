using System;
using System.Net.Http;
using System.Text;
using Xamarin.Forms;

namespace App1
{
    public struct Driver
    {
        /*public Driver(string str = "")
        {
            name = "";
            lastname = "";
            patronymic = "";
            pasp = "";
            DrLic = "";
            city = "";
            phone = "";



            paspPh1 = "";
            paspPh2 = "";
            paspPh3 = "";

            drLicPh1 = "";
            drLicPh2 = "";

            regCertPh1 = "";
            regCertPh2 = "";

            mark = "";
            model = "";
            dataCar = "";
            color = "";
            vin = "";
            regNumberCar = "";
            carNumber = "";

            card = "";
        }*/
        public int companies;
        public string name, lastname, patronymic;
        public string city;
        public string phone;

        public string paspNumber;
        public string paspData;
        public string paspOrg;
        public string paspCode;

        public string paspPh1;
        public string paspPh2;
        public string paspPh3;

        public string drLicPh1;
        public string drLicPh2;

        public string regCertPh1;
        public string regCertPh2;

        public string drLicNumber;
        public string drLicPeriod;
        public string drLicData;

        public string mark;
        public string model;
        public string dataCar;
        public string color;
        public string vin;
        public string regNumberCar;
        public string carNumber;

        public string card;
    }

    public struct Adv
    {
        public string name;
        public string city;
        public string phone;
        public string address;
        public string ogrn;
        public string inn;
        public string kpp;

        public string fioDir;
        public string dirPost;
        public string fioCont;
        public string contPost;

        public string BankName;
        public string BankAddress;
        public string BunkNumberR;
        public string BunkNumberK;
        public string BIK;

        public int type;
    }

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

            //var client = new HttpClient();
            //var uri = new Uri(url);
            //Stream respStream = await client.GetStreamAsync(uri);
            //DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(rootObject));
            //rootObject feed = (rootObject)ser.ReadObject(respStream);
        }
    }
}
