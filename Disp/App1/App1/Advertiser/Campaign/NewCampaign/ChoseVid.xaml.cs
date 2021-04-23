using App1.Advertiser.Campaing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App1.Domain;

using Newtonsoft.Json;
using System.Net.Http;

namespace App1.Advertiser.Campaign.NewCampaign
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChoseVid : ContentPage
    {
        Adv nowUser;
        public ChoseVid(Adv now)
        {
            nowUser = now;
            InitializeComponent();

            loadVideos();
        }

        private async void loadVideos()
        {
            HttpClient client = new HttpClient();

            var answer = await client.GetAsync(Server.url + "video/?id=" + nowUser.id);
            var responseBody = await answer.Content.ReadAsStringAsync();

            List<VideoObj> list = JsonConvert.DeserializeObject<List<VideoObj>>(responseBody);

            videos.Children.Clear();

            if (list != null && list.Count != 0)
            {
                foreach (var item in list)
                {
                    if (item.validated)
                    {
                        Vid vid = new Vid() { Name = item.name, Url = item.url };
                        vid.Margin = new Thickness(5, 5, 5, 5);

                        Button btn = new Button();
                        btn.HeightRequest = 50;
                        btn.Text = "Выбрать";
                        btn.Margin = new Thickness(5, 5, 5, 5);

                        btn.Clicked += async delegate
                        {
                            Compaign compaign = new Compaign();
                            compaign.adv = nowUser;
                            compaign.video = item;

                            await Navigation.PushAsync(new ChoseTarif(compaign));
                        };

                        BoxView fr = new BoxView();
                        fr.HeightRequest = 1;
                        fr.BackgroundColor = Color.Gray;

                        videos.Children.Add(vid);
                        videos.Children.Add(btn);
                        videos.Children.Add(fr);
                    } 
                }
            }
        }
    }
}