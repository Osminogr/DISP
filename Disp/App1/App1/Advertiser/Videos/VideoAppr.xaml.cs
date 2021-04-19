using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1.Advertiser
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VideoAppr : ContentPage
    {
        Adv nowUser;
        public VideoAppr(Adv now)
        {
            nowUser = now;
            InitializeComponent();

            Request();

        }

        public async void Request()
        {

            HttpClient client = new HttpClient();
            var i = 0;
            var answer = await client.GetAsync(Server.url + "video/?phone=" + nowUser.phone);
            var responseBody = await answer.Content.ReadAsStringAsync();

            var dictionary = responseBody
            .Split(',')
            .ToList<string>();
            foreach (var x in dictionary)
            {

                videos.Children.Add(new Vid { Name = x, Url = "http://46.101.167.149:8000/media/Pexels_Videos_2313069_iwOTG0H.mp4" });
                }
        }
        private async void ToAct(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new VideosAct(nowUser));
        }
    }
}