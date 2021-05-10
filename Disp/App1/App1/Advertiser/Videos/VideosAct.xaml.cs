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
        int count = 0;
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

                            Request(true);
                        }
                    }
                })
            });

            Request(true);
        }

        public async void Request(bool showActive)
        {
            videosAct.Children.Clear();
            videosAppr.Children.Clear();
            count = 0;

            List<Video> list = await Server.GetVideos(nowUser, false);

            bool loaded = false;

            if (list != null && list.Count != 0)
            {
                foreach (var item in list)
                {
                    if (showActive)
                    {
                        if (!item.validated)
                        {
                            loaded = true;
                            videosAct.Children.Add(new VideoTemplate() { Name = item.name, Url = item.url, Margin = new Thickness(0, 10, 0, 0) });
                            count++;
                        }
                    }
                    else
                    {
                        if (item.validated)
                        {
                            loaded = true;
                            videosAppr.Children.Add(new VideoTemplate() { Name = item.name, Url = item.url, Margin = new Thickness(0, 10, 0, 0) });
                            count++;
                        }
                    }
                }

                OverrideTitleView("Видеоролики", count);
            }

            if (!loaded)
            {
                Label label = new Label();
                label.Text = "У Вас нет загруженных видеороликов";
                label.HorizontalOptions = LayoutOptions.Center;
                label.VerticalOptions = LayoutOptions.Center;

                if (showActive) videosAct.Children.Add(label);
                else videosAppr.Children.Add(label);

                OverrideTitleView("Видеоролики(0)", -1);
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
            Request(false);
            BtnAddVideoDialog.IsVisible = false;
        }

        private void ToAct(object sender, EventArgs e)
        {
            act.IsVisible = true;
            appr.IsVisible = false;
            appeared.TextColor = Color.FromHex("#BCBCBC");
            loaded.TextColor = Color.FromHex("#F39F26");
            Request(true);
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