using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;

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

            var answer = await client.GetAsync(Server.url + "video/?phone=" + nowUser.phone);
            var responseBody = await answer.Content.ReadAsStringAsync();

            List<VideoObj> list = JsonConvert.DeserializeObject<List<VideoObj>>(responseBody);

            if (list != null && list.Count != 0)
            {
                foreach (var item in list)
                {
                    if (item.is_validated)
                        videos.Children.Add(new Vid { Name = item.video, Url = App1.Properties.Resources.urlMedia + item.video });
                }
            }
        }

        private async void ToAct(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new VideosAct(nowUser));
        }
    }
}