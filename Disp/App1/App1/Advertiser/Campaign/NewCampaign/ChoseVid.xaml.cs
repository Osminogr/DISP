﻿using App1.Advertiser.Campaign;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App1.Domain;
using App1.Utils;

using Newtonsoft.Json;
using System.Net.Http;
using Octane.Xamarin.Forms.VideoPlayer;

namespace App1.Advertiser.Campaign.NewCampaign
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChoseVid : ContentPage
    {
        Compaign nowUser;
        List<View> frames;
        public ChoseVid(Compaign now)
        {
            nowUser = now;
            InitializeComponent();

            loadVideos();
        }

        private void OverrideTitleView(string name, string nameAction, int count)
        {
            NavigationPage.SetTitleView(this, TitleView.OverrideGridView(name, nameAction, count, new Command(() => {
                Navigation.PushAsync(new Confirm(nowUser));
            })));
        }

        private async void loadVideos()
        {
            HttpClient client = new HttpClient();

            var answer = await client.GetAsync(Server.url + "video/?id=" + nowUser.adv.id);
            var responseBody = await answer.Content.ReadAsStringAsync();

            List<Video> list = JsonConvert.DeserializeObject<List<Video>>(responseBody);

            if (list != null && list.Count != 0)
            {
                int count = 0;
                frames = new List<View>();
                foreach (var item in list)
                {
                    if (item.validated)
                    {
                        count++;

                        Frame frame = new Frame() { Margin = new Thickness(0, 10, 0, 0), Padding = new Thickness(10, 0, 10, 0), BackgroundColor = Color.Transparent };

                        VideoPlayer videoPlayer = new VideoPlayer();
                        videoPlayer.Source = item.url;
                        videoPlayer.DisplayControls = true;
                        videoPlayer.AutoPlay = false;
                        videoPlayer.Margin = new Thickness(0, -6, 0, 0);
                        videoPlayer.HeightRequest = 150;

                        Label label = new Label();
                        label.Text = item.name;
                        label.HeightRequest = 35;
                        label.TextColor = Color.White;
                        label.Padding = new Thickness(10, 7, 0, 0);
                        label.BackgroundColor = Color.LightGray;

                        StackLayout sl = new StackLayout() { Padding = new Thickness(10, 0, 10, 10), Margin = new Thickness(0, 0, 0, 0) };

                        sl.Children.Add(label);
                        sl.Children.Add(videoPlayer);
                        frame.Content = sl;

                        frame.GestureRecognizers.Add(new TapGestureRecognizer()
                        {
                            Command = new Command(() => {
                                foreach (var fr in frames)
                                    ((Label)fr).BackgroundColor = Color.LightGray;

                                label.BackgroundColor = Color.FromHex("#FFB800");
                                nowUser.video = item;
                            })
                        });

                        videos.Children.Add(frame);
                        frames.Add(label);
                    } 
                }

                OverrideTitleView("Видеоролики", "Дальше", count);
            }
        }
    }
}