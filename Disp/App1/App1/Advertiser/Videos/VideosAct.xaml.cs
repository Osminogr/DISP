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
using App1.Utils;
using App1.Templates;
using Plugin.Media;
using Plugin.Media.Abstractions;

namespace App1.Advertiser
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VideosAct : ContentPage
    {
        Adv nowUser;
        int countValid = 0;
        int countInValid = 0;
        public VideosAct(Adv now)
        {
            nowUser = now;
            InitializeComponent();

            LoadVideoLabel.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(async () =>
                {
                    if (CrossMedia.Current.IsPickVideoSupported)
                    {
                        MediaFile video = await CrossMedia.Current.PickVideoAsync();

                        if (video != null)
                        {
                            Video videoObject = new Video();
                            videoObject.name = Path.GetFileName(video.Path);
                            videoObject.data = video.GetStream();
                            videoObject.path = video.Path;
                            videoObject.adv = nowUser;
                            await Server.LoadVideoAsync(videoObject);

                            AddVideoDialog.IsEnabled = false;
                            AddVideoDialog.IsVisible = false;

                            Request();
                        }
                    }
                })
            });

            Request();
        }

        public async void Request()
        {
            countInValid = 0;
            countValid = 0;

            HttpClient client = new HttpClient();

            var answer = await client.GetAsync(Server.ROOT_URL + "video/?id=" + nowUser.id);
            var responseBody = await answer.Content.ReadAsStringAsync();

            List<Video> list = JsonConvert.DeserializeObject<List<Video>>(responseBody);

            videosAct.Children.Clear();
            videosAppr.Children.Clear();

            bool loaded = false;

            if (list != null && list.Count != 0)
            {
                loaded = true;
                
                foreach (var item in list)
                {
                    if (!item.validated)
                    {
                        videosAct.Children.Add(new VideoTemplate() { Name = item.name, Url = item.url, Margin = new Thickness(0, 10, 0, 0) });
                        countInValid++;
                    }
                    else
                    {
                        videosAppr.Children.Add(new VideoTemplate() { Name = item.name, Url = item.url, Margin = new Thickness(0, 10, 0, 0) });
                        countValid++;
                    }
                }

                OverrideTitleView("Видеоролики", countValid);
            }

            if (!loaded)
            {
                Label l = new Label();
                l.Text = "У Вас нет загруженных видеороликов";
                l.HorizontalOptions = LayoutOptions.Center;
                l.VerticalOptions = LayoutOptions.Center;
                videosAct.Children.Add(new Label());
                videosAppr.Children.Add(new Label());

                OverrideTitleView("Видеоролики", -1);
            }
        }

        private void OverrideTitleView(string name, int count)
        {
            NavigationPage.SetTitleView(this, TitleView.OverrideView(name, count));
        }

        private void ToAppr(object sender, EventArgs e)
        {
            act.IsVisible = false;
            appr.IsVisible = true;
            loaded.TextColor = Color.FromHex("#BCBCBC");
            appeared.TextColor = Color.FromHex("#F39F26");
            OverrideTitleView("Видеоролики", countInValid);
            BtnAddVideoDialog.IsVisible = false;
        }

        private void ToAct(object sender, EventArgs e)
        {
            act.IsVisible = true;
            appr.IsVisible = false;
            appeared.TextColor = Color.FromHex("#BCBCBC");
            loaded.TextColor = Color.FromHex("#F39F26");
            OverrideTitleView("Видеоролики", countValid);
            BtnAddVideoDialog.IsVisible = true;
        }

        private void NewVideo(object sender, EventArgs e)
        {
            AddVideoDialog.IsEnabled = true;
            AddVideoDialog.IsVisible = true;
        }

        public void CloseAddVideoDialog(object sender, EventArgs e)
        {
            AddVideoDialog.IsEnabled = false;
            AddVideoDialog.IsVisible = false;
        }

        private async void RequestVideo(object sender, EventArgs e)
        {
            AddVideoDialog.IsEnabled = false;
            AddVideoDialog.IsVisible = false;

            await Navigation.PushAsync(new VideosRequest(nowUser));

        }
    }
}