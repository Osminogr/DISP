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
using App1.Domain;


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

            var answer = await client.GetAsync(Server.url + "video/?id=" + nowUser.id);
            var responseBody = await answer.Content.ReadAsStringAsync();

            List<VideoObj> list = JsonConvert.DeserializeObject<List<VideoObj>>(responseBody);

            videosAct.Children.Clear();
            videosAppr.Children.Clear();

            bool loaded = false;

            if (list != null && list.Count != 0)
            {
                loaded = true;
                foreach (var item in list)
                {
                    if (!item.validated)
                        videosAct.Children.Add(new Vid() { Name = item.name, Url = item.url });
                    else
                        videosAppr.Children.Add(new Vid() { Name = item.name, Url = item.url });
                }
            }

            if (!loaded)
            {
                Label l = new Label();
                l.Text = "У Вас нет загруженных видеороликов";
                l.HorizontalOptions = LayoutOptions.Center;
                l.VerticalOptions = LayoutOptions.Center;
                videosAct.Children.Add(new Label());
                videosAppr.Children.Add(new Label());
            }
        }

        private void ToAppr(object sender, EventArgs e)
        {
            act.IsVisible = false;
            appr.IsVisible = true;
            loaded.BackgroundColor = Color.FromHex("#0B0000AA");
            appeared.BackgroundColor = Color.FromHex("#00000000");
        }

        private void ToAct(object sender, EventArgs e)
        {
            act.IsVisible = true;
            appr.IsVisible = false;
            appeared.BackgroundColor = Color.FromHex("#0B0000AA");
            loaded.BackgroundColor = Color.FromHex("#00000000");
        }

        private async void NewVideo(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Loader(nowUser));
        }
    }
}