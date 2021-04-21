using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;


namespace App1.Advertiser
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VideosAct : ContentPage
    {
        Adv nowUser;
        public VideosAct(Adv now)
        {
            nowUser = now;
            InitializeComponent();
            Request();
        }

        public async void Request()
        {
            HttpClient client = new HttpClient();

            var answer = await client.GetAsync(Server.url + "video/?phone=" + nowUser.phone);
            var responseBody = await answer.Content.ReadAsStringAsync();

            List<VideoObj> list = JsonConvert.DeserializeObject<List<VideoObj>>(responseBody);

            videos.Children.Clear();

            bool loaded = false;

            if (list != null && list.Count != 0)
            {
                loaded = true;
                foreach (var item in list)
                {
                    if (!item.is_validated)
                        videos.Children.Add(new Vid() { Name = item.video, Url = "http://46.101.167.149:8000/media/" + item.video });
                }
            }

            if (!loaded)
            {
                Label l = new Label();
                l.Text = "У Вас нет загруженных видеороликов";
                l.HorizontalOptions = LayoutOptions.Center;
                l.VerticalOptions = LayoutOptions.Center;
                videos.Children.Add(new Label());
            }
        }

        private async void ToAppr(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new VideoAppr(nowUser));
        }

        private async void NewVideo(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Loader(nowUser));
        }
    }
}