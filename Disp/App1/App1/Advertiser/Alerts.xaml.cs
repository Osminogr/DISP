
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net.Http;
using System;
using System.Collections.Generic;
using App1.Domain;
using App1.Utils;

namespace App1.Advs
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Alerts : ContentPage
    {
        Adv nowUser;
        public Alerts(Adv now)
        {
            InitializeComponent();
            nowUser = now;
            Request();

            OverrideTitleView("Оповещения", -1);
        }

        public async void Request()
        {
            HttpClient client = new HttpClient();

            var answer = await client.GetAsync(Server.ROOT_URL + "alert/?phone=" + nowUser.company.phone.Trim('"'));
            var responseBody = await answer.Content.ReadAsStringAsync();
            char[] sym = new char[] { '[', ']', '{', ',' };
            foreach (var ch in sym)
            {
                responseBody = responseBody.Replace(ch.ToString(), "");
            }
            string[] str = new string[] { "\\n", "\\r", "\\" };
            foreach (var c in str)
            {
                responseBody = responseBody.Replace(c.ToString(), " ");
            }
            responseBody = responseBody.Replace("text\":", " ").Trim(' ');
            Console.WriteLine(responseBody);
            Console.WriteLine(Server.ROOT_URL + "alert/?phone=" + nowUser.company.phone);
            var dictionary = responseBody.Trim('}').Split('}');
        }

        private void OverrideTitleView(string name, int count)
        {
            NavigationPage.SetTitleView(this, TitleView.OverrideView(name, count));
        }
    }
}